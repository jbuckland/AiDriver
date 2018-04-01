using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrive : MonoBehaviour
{

    private Vector3 moveDirection;
    public float speed;
    public float maxRotation;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        var rotationDirection = Random.Range(-1f, 1f);
        var rotation = maxRotation * rotationDirection;

        transform.Rotate(new Vector3(0, rotation * Time.deltaTime, 0));


    }
}
