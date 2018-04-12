using System;
using System.Collections.Generic;
using UnityEngine;

public class CarObject : MonoBehaviour, ICarObject
{
    //see this article for more ideas
    //https://www.codeproject.com/Articles/1220644/ReInventing-Neural-Networks-Part
    public float maxTurn;
    public float maxSpeed;
    public Rigidbody carBody;
    public bool manualControl;

    private bool drivingEnabled = true;
    public CarBrainController BrainController;

    private void Start()
    {
    }


    void FixedUpdate()
    {
        BrainController.FixedUpdate(Time.deltaTime);
    }

    public CarSensorData GetSensorData()
    {
        var data = new CarSensorData();
        var frontDist = float.MaxValue;
        var frontLeftDist = float.MaxValue;
        var frontRightDist = float.MaxValue;
        var leftDist = float.MaxValue;
        var rightDist = float.MaxValue;
        var currentSpeed = float.MaxValue;

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

        data.FrontDistance = (decimal) frontDist;
        data.FrontLeftDistance = (decimal) frontLeftDist;
        data.FrontRightDistance = (decimal) frontRightDist;
        data.LeftDistance = (decimal) leftDist;
        data.RightDistance = (decimal) rightDist;
        data.Speed = (decimal) currentSpeed;
        return data;
    }

    public float GetHorizontalInput()
    {
        return Input.GetAxis("Horizontal");
    }

    public float GetVerticalInput()
    {
        return Input.GetAxis("Vertical");
    }

    public void Move(float horizInput, float vertInput, float deltaTime)
    {
        var rotation = maxTurn * horizInput * deltaTime;
        transform.Rotate(new Vector3(0, rotation, 0));
        var moveForce = new Vector3(0, 0, maxSpeed * vertInput * deltaTime);
        carBody.AddRelativeForce(moveForce, ForceMode.VelocityChange);
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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            BrainController.HitWall();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            var checkPoint = other.gameObject.GetComponent<CheckPoint>();
            var id = checkPoint.CheckPointId;
            BrainController.HitCheckpoint(id);
        }
    }
}