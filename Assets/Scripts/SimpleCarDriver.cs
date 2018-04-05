using System;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarDriver : MonoBehaviour
{
    //see this article for more ideas
    //https://www.codeproject.com/Articles/1220644/ReInventing-Neural-Networks-Part


    public float maxTurn;
    public float maxSpeed;
    public Rigidbody carBody;
    public GameController controller;
    public bool manualControl = true;
    public string id;

    public bool IsAlive { get; private set; }


    private bool drivingEnabled = true;
    private List<int> touchedCheckPoints = new List<int>();
    private float score = 0f;
    private const int POINTS_PER_CHECKPOINT = 1;

    private SimpleCarBrain brain;
    private float frontDist;
    private float frontLeftDist;
    private float frontRightDist;
    private float leftDist;
    private float rightDist;
    private float currentSpeed;

    private float KILL_TIME = 10;
    private float timeSinceLastScore = 0;

    private void Start()
    {
        id = Guid.NewGuid().ToString().Substring(0, 5);
        brain = new SimpleCarBrain();
        IsAlive = true;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsAlive)
        {
            GetDistances();
            float horizontalInput = 0;
            float verticalInput = 0;
            if (manualControl == true)
            {
                horizontalInput = Input.GetAxis("Horizontal");
                verticalInput = Input.GetAxis("Vertical");
            }
            else
            {
                brain.Evaluate((decimal) frontDist,
                    (decimal) frontLeftDist,
                    (decimal) frontRightDist,
                    (decimal) leftDist,
                    (decimal) rightDist,
                    (decimal) currentSpeed);

                horizontalInput = (float) brain.Speed;
                verticalInput = (float) brain.Turn;
            }


            Move(horizontalInput, verticalInput);
            CheckForTimeoutKill(Time.deltaTime);
        }
    }

    private void CheckForTimeoutKill(float deltaTime)
    {
        //kill the car if it's score doesn't get better for a fixed number of seconds
        timeSinceLastScore += deltaTime;
        if (manualControl == false && timeSinceLastScore > KILL_TIME)
        {
            IsAlive = false;
        }
    }

    private void GetDistances()
    {
        frontDist = float.MaxValue;
        frontLeftDist = float.MaxValue;
        frontDist = float.MaxValue;
        leftDist = float.MaxValue;
        rightDist = float.MaxValue;


        var forwardVector = transform.TransformVector(Vector3.forward);
        var forwardLeftVector = transform.TransformVector(new Vector3(-1, 0, 2));
        var forwardRightVector = transform.TransformVector(new Vector3(1, 0, 2));
        var leftVector = transform.TransformVector(new Vector3(-2, 0, 1));
        var rightVector = transform.TransformVector(new Vector3(2, 0, 1));

        CastRay(forwardVector, ref frontDist);
        CastRay(forwardLeftVector, ref frontLeftDist);
        CastRay(forwardRightVector, ref frontRightDist);
        CastRay(leftVector, ref leftDist);
        CastRay(rightVector, ref rightDist);
    }


    private void CastRay(Vector3 directionVector, ref float distHolder)
    {
        RaycastHit hitInfo;
        Physics.queriesHitTriggers = false;
        if (Physics.Raycast(transform.position, directionVector, out hitInfo))
        {
            distHolder = hitInfo.distance;
            Debug.DrawLine(transform.position, hitInfo.point);
        }
    }

    public void SetDistances()
    {
        controller.setFrontDist(frontDist);
        controller.setFrontLeftDist(frontLeftDist);
        controller.setFrontRightDist(frontRightDist);
        controller.setLeftDist(leftDist);
        controller.setRightDist(rightDist);
    }


    private void Move(float horizInput, float vertInput)
    {
        var rotation = maxTurn * horizInput * Time.deltaTime;
        transform.Rotate(new Vector3(0, rotation, 0));
        controller.SetTurnInput(horizInput);

        var moveForce = new Vector3(0, 0, maxSpeed * vertInput * Time.deltaTime);
        carBody.AddRelativeForce(moveForce, ForceMode.VelocityChange);
        currentSpeed = vertInput;
        controller.SetSpeedInput(vertInput);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            IsAlive = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            var checkPoint = other.gameObject.GetComponent<CheckPoint>();
            var id = checkPoint.CheckPointId;
            //Debug.Log("We hit checkpoint " + id);

            //if we have touched all other checkpoints, and we're now touching the start
            //reset checkpoints touched, and add start line.
            if (id == GameController.START_CHECKPOINT_ID && touchedCheckPoints.Count == GameController.CHECKPOINT_COUNT)
            {
                touchedCheckPoints.Clear();
                ScoreCheckPoint(id);
            }
            //if we haven't touched this checkpoint yet, record that we've touched it
            else if (!touchedCheckPoints.Contains(id))
            {
                ScoreCheckPoint(id);
            }
        }
    }

    private void ScoreCheckPoint(int id)
    {
        touchedCheckPoints.Add(id);
        score += POINTS_PER_CHECKPOINT;
        timeSinceLastScore = 0;
    }

    public float GetScore()
    {
        return score;
        //ideas:

        //get to the next checkpoint, quickly.
        //can easily do a running score: just cumulative time, lower is better

        //get far away from the last checkpoint, quickly.

        //touch each checkpoint once, quickly. reset when all are touched
        //score = #checkpointsTouched
    }
}