using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriver : MonoBehaviour
{

    public float motorForce;
    public float steerForce;
    public float brakeForce;
    public WheelCollider wheelFrontRight;
    public WheelCollider wheelFrontLeft;
    public WheelCollider wheelRearRight;
    public WheelCollider wheelRearLeft;




    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical") * motorForce;
        float h = Input.GetAxis("Horizontal") * steerForce;

        //Debug.Log("CarDriver: v=" + v + ", h=" + h);

        wheelRearRight.motorTorque = v;
        wheelRearLeft.motorTorque = v;

        wheelFrontRight.steerAngle = h;
        wheelFrontLeft.steerAngle = h;

        if (Input.GetKey(KeyCode.Space))
        {
            wheelRearRight.brakeTorque = brakeForce;
            wheelRearLeft.brakeTorque = brakeForce;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            wheelRearRight.brakeTorque = 0;
            wheelRearLeft.brakeTorque = 0;
        }


    }
}
