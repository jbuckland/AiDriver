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



    private bool drivingEnabled = true;
    private List<int> touchedCheckPoints = new List<int>();
    private float score = 0f;
    private const int POINTS_PER_CHECKPOINT = 1;



    // Update is called once per frame
    void Update()
    {
        if (drivingEnabled)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            var rotation = maxTurn * horizontalInput * Time.deltaTime;
            transform.Rotate(new Vector3(0, rotation, 0));
            controller.SetTurnInput(horizontalInput);


            var moveForce = new Vector3(0, 0, maxSpeed * verticalInput * Time.deltaTime);
            carBody.AddRelativeForce(moveForce, ForceMode.VelocityChange);
            controller.SetSpeedInput(verticalInput);

        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "Wall")
        {
            //drivingEnabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Checkpoint")
        {

            var checkPoint = other.gameObject.GetComponent<CheckPoint>();
            var id = checkPoint.CheckPointId;
            Debug.Log("We hit checkpoint " + id);

            //if we have touched all other checkpoints, and we're now touching the start
            //reset checkpoints touched, and add start line.
            if (id == GameController.START_CHECKPOINT_ID && touchedCheckPoints.Count == GameController.CHECKPOINT_COUNT)
            {
                touchedCheckPoints.Clear(); ;
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
        Debug.Log("score increased to " + score);
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



    //kill the car is it's score doesn't get better for a fixed number of seconds


}
