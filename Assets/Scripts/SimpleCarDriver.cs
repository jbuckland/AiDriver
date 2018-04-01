using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarDriver : MonoBehaviour
{

    public float maxTurn;
    public float maxSpeed;
    public Rigidbody carBody;

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        var rotation = maxTurn * horizontalInput * Time.deltaTime;
        if (rotation > 0f)
        {
            Debug.Log("rotating " + rotation);
        }
        transform.Rotate(new Vector3(0, rotation, 0));


        var moveForce = new Vector3(0, 0, maxSpeed * verticalInput * Time.deltaTime);
        carBody.AddRelativeForce(moveForce, ForceMode.VelocityChange);


    }
}
