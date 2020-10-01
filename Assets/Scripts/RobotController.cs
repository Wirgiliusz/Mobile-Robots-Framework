﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RobotController : MonoBehaviour
{
    public MotorsController MC_R;
    public MotorsController MC_L;
    public SensorController SC;

    public UIController UI;

    private int cameraIterator = 0;
    public Camera robotCamera;
    public Camera overheadCamera;
    public Camera freeCamera;

    public TrailRenderer path;

    public float speed_change;
    public float speedR_change;
    public float speedL_change;

    // Start is called before the first frame update
    void Start()
    {
        overheadCamera.enabled = false;
        freeCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkForKeyPresses();
        Debug.Log("SpeedL: "+ MC_L.getSpeed() + " SpeedR: "+ MC_R.getSpeed() + " | Velocity: " + String.Format("{0:0.00}", this.GetComponentInChildren<Rigidbody>().velocity.magnitude) + " m/s" + string.Format(" | Sensor:  {0:0.00} ", SC.getHitDistance()));
        
        UI.setVelocityText(this.GetComponentInChildren<Rigidbody>().velocity.magnitude);
        UI.setMotorLText(MC_L.getSpeed());
        UI.setMotorRText(MC_R.getSpeed());
        UI.setSensorText(SC.getHitDistance());
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
        *   C:                      Change camera view              [C]
        *   T:                      Show/Hide travel path           [T]
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

        if (Input.GetKeyDown(KeyCode.C)) {
            switchCamera();
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            togglePath();
        }
    }

    void switchCamera() {
        cameraIterator = cameraIterator == 2 ? 0 : ++cameraIterator;

        switch(cameraIterator) {
            case 0:
                robotCamera.enabled = true; 
                overheadCamera.enabled = false;
                freeCamera.enabled = false;
                break;
            case 1:
                overheadCamera.enabled = true;
                robotCamera.enabled = false; 
                freeCamera.enabled = false;
                break;
            case 2:
                freeCamera.enabled = true;
                overheadCamera.enabled = true;
                robotCamera.enabled = false; 
                break;

        }
        /*
        if (robotCamera.enabled == true) {
            overheadCamera.enabled = true;
            robotCamera.enabled = false;
        } else if (overheadCamera.enabled == true) {
            robotCamera.enabled = true;
            overheadCamera.enabled = false;
        } 
        */
    }

    void togglePath() {
        if (path.enabled == true) {
            path.enabled = false;
        } else if (path.enabled == false) {
            path.enabled = true;
        }
    }
}
