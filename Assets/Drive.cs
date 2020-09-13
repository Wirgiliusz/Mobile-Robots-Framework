using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Drive : MonoBehaviour
{
    public WheelCollider WC;
    public float torque = 50;

    // Start is called before the first frame update
    void Start()
    {
        WC = this.GetComponent<WheelCollider>();
    }


    void go(float accel) {
        accel = Mathf.Clamp(accel, -1, 1);
        float thrustTorque = accel * torque;
        WC.motorTorque = thrustTorque;
    }

    // Update is called once per frame
    void Update()
    {
        float acceleration = Input.GetAxis("Vertical");
        go(acceleration);
        Debug.Log("Velocity: " + String.Format("{0:0.00}", this.GetComponentInParent<Rigidbody>().velocity.z) + " m/s");
    }
}
