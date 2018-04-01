using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleCarDriver : MonoBehaviour
{

    public float maxTurn;
    public float maxSpeed;
    public Rigidbody carBody;

    public Text speedText;
    public Text turnText;

    private bool enabled = true;

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            var rotation = maxTurn * horizontalInput * Time.deltaTime;
            if (rotation > 0f)
            {
                Debug.Log("rotating " + rotation);
            }
            transform.Rotate(new Vector3(0, rotation, 0));
            turnText.text = (horizontalInput * 100).ToString("0");


            var moveForce = new Vector3(0, 0, maxSpeed * verticalInput * Time.deltaTime);
            carBody.AddRelativeForce(moveForce, ForceMode.VelocityChange);
            speedText.text = (verticalInput * 100).ToString("0");
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "Wall")
        {
            enabled = false;
        }
    }

}
