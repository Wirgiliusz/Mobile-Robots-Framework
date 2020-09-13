using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Drive : MonoBehaviour
{
    public WheelCollider WC;
    private float speed = 0;
    private float speedR = 0;
    private float speedL = 0;

    // Start is called before the first frame update
    void Start()
    {
        WC = this.GetComponent<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            speed += 1;
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            speed -= 1;
        } else if (Input.GetKeyDown(KeyCode.Space)) {
            speed = 0;
            speedR = 0;
            speedL = 0;
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            speedR += 1;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            speedL += 1;
        }

        if (this.name == "WheelR") {
            WC.motorTorque = speed + speedR;
        } else if (this.name == "WheelL") {
            WC.motorTorque = speed + speedL;
        }

        Debug.Log("Speed: "+ speed + " SpeedR: "+ speedR + " SpeedL: "+ speedL + " Velocity: " + String.Format("{0:0.00}", this.GetComponentInParent<Rigidbody>().velocity.magnitude) + " m/s");
    }
}
