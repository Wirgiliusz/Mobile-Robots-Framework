using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualController : MonoBehaviour
{
    private RobotController RC;

    private MotorController MC_L;
    private MotorController MC_R;

    public float speed_change;
    public float speedR_change;
    public float speedL_change;

    // Start is called before the first frame update
    void Start()
    {
        RC = GetComponent<RobotController>();
        MC_L = RC.getMotorsControllers()[0];
        MC_R = RC.getMotorsControllers()[1];
    }

    // Update is called once per frame
    void Update()
    {
        checkForKeyPresses();
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
}
