using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RobotController : MonoBehaviour
{
    public MotorsController MC_R;
    public MotorsController MC_L;
    public SensorController SC;
    public UIController UI;

    public float speed_change;
    public float speedR_change;
    public float speedL_change;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
