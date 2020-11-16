﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RobotController : MonoBehaviour
{
    public MotorsController MC_R;
    public MotorsController MC_L;
    public SensorController SC;

    public TrailRenderer travelPath;

    public float speed_change;
    public float speedR_change;
    public float speedL_change;

    private float leftMotorSpeed = 0;
    private float leftMotorSpeedPercent = 0;
    private float rightMotorSpeed = 0;
    private float rightMotorSpeedPercent = 0;
    private float robotVelocity = 0;
    private float sensorReading = 0;

    private Rigidbody robotRb;

    // Start is called before the first frame update
    void Start()
    {
        robotRb = this.GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        checkForKeyPresses();
        readInputs();
    }

    void checkForKeyPresses() {
        /*  Robot control keybinds
        *   Up arrow:               + speed_change for both wheels  [/\]
        *   Down arrow:             - speed_change for both wheels  [\/]
        *   Left arrow:             + speedL_change for left wheel  [<-]
        *   Shift + Left arrow:     - speedL_change for left wheel  [S + <-]
        *   Right arrow:            + speedR_change for right wheel [->]
        *   Shift + Right arrow:    - speedR_change for right wheel [S + ->]
        *   Space:                  Reset all speeds to 0 and brake [_]
        */
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            MC_R.addSpeed(speed_change);
            MC_L.addSpeed(speed_change);
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            MC_R.addSpeed(-speed_change);
            MC_L.addSpeed(-speed_change); 
        } else if (Input.GetKeyDown(KeyCode.Space)) {
            MC_R.setSpeed(0);
            MC_L.setSpeed(0);
            MC_R.setBrake(true);
            MC_L.setBrake(true);
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftShift)) {
            MC_R.addSpeed(speedR_change);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.LeftShift)) {
            MC_L.addSpeed(speedL_change);
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift)) {
            MC_R.addSpeed(-speedR_change);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftShift)) {
            MC_L.addSpeed(-speedL_change);
        }
        if (!Input.GetKey(KeyCode.Space)) {
            MC_R.setBrake(false);
            MC_L.setBrake(false);
        }
    }

    void readInputs() {
        leftMotorSpeed = MC_L.getSpeed();
        leftMotorSpeedPercent = MC_L.getSpeedPercent();
        rightMotorSpeed = MC_R.getSpeed();
        rightMotorSpeedPercent = MC_R.getSpeedPercent();
        robotVelocity = robotRb.velocity.magnitude;
        sensorReading = SC.getHitDistance();
    }

    public float getLeftMotorSpeed() {
        return leftMotorSpeed;
    }
    public float getLeftMotorSpeedPercent() {
        return leftMotorSpeedPercent;
    }
    public float getRightMotorSpeed() {
        return rightMotorSpeed;
    }
    public float getRightMotorSpeedPercent() {
        return rightMotorSpeedPercent;
    }
    public float getRobotVelocity() {
        return robotVelocity;
    }
    public float getSensorReading() {
        return sensorReading;
    }

    public void togglePath() {
        if (travelPath.enabled == true) {
            travelPath.enabled = false;
        } else if (travelPath.enabled == false) {
            travelPath.enabled = true;
        }
    }
}
