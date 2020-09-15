using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Drive : MonoBehaviour
{
    public WheelCollider WC;
    public GameObject WheelModel;

    // Speeds of different wheels
    private float speed = 0;    // speed of both wheels
    private float speedR = 0;   // speed of right wheel
    private float speedL = 0;   // speed of left wheel

    // Value of speed change for different wheels
    public float speed_change;
    public float speedR_change;
    public float speedL_change;

    // Start is called before the first frame update
    void Start()
    {
        WC = this.GetComponent<WheelCollider>();
    }


    void checkForKeyPresses() {
        /*  Wheels control keybinds
        *   Up arrow:               + speed_change for both wheels  [/\]
        *   Down arrow:             - speed_change for both wheels  [\/]
        *   Left arrow:             + speedL_change for left wheel  [<-]
        *   Shift + Left arrow:     - speedL_change for left wheel  [S + <-]
        *   Right arrow:            + speedR_change for right wheel [->]
        *   Shift + Right arrow:    - speedR_change for right wheel [S + ->]
        *   Space:                  Reset all speeds to 0 and brake [_]
        */
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            speed += speed_change;
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            speed -= speed_change;
        } else if (Input.GetKeyDown(KeyCode.Space)) {
            speed = 0;
            speedR = 0;
            speedL = 0;
            WC.brakeTorque = Mathf.Infinity;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftShift)) {
            speedR += speedR_change;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.LeftShift)) {
            speedL += speedL_change;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift)) {
            speedR -= speedR_change;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftShift)) {
            speedL -= speedL_change;
        }
        if (!Input.GetKey(KeyCode.Space)) {
            WC.brakeTorque = 0;
        }
    }

    void addSpeedToWheels() {
        // Distinguish which wheel should get which speed change
        if (this.name == "WheelR") {
            WC.motorTorque = speed + speedR;
        } else if (this.name == "WheelL") {
            WC.motorTorque = speed + speedL;
        }
    }

    void updateWheelsModelsRotation() {
        Quaternion rotation;
        Vector3 position;
        WC.GetWorldPose(out position, out rotation);
        rotation = rotation * Quaternion.Euler(new Vector3(0, 0, 90));
        WheelModel.transform.position = position;
        WheelModel.transform.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        checkForKeyPresses();        
        addSpeedToWheels();
        updateWheelsModelsRotation();

        // Log different speeds
        Debug.Log("Speed: "+ speed + " SpeedR: "+ speedR + " SpeedL: "+ speedL + " Velocity: " + String.Format("{0:0.00}", this.GetComponentInParent<Rigidbody>().velocity.magnitude) + " m/s");
    }
}
