using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramMaster : MonoBehaviour
{
    public UIController UI;

    private int cameraIterator = 0; // Index of currently used camera
    public Camera robotCamera;
    public Camera overheadCamera;
    public Camera freeCamera;

    public RobotController RC;

    // Start is called before the first frame update
    void Start()
    {
        overheadCamera.enabled = false;
        freeCamera.enabled = false;  
    }

    // Update is called once per frame
    void Update()
    {
        /* Global program keybinds
        * C:    Switch camera       [C]
        * T:    Toggle travel path  [T]
        */
        if (Input.GetKeyDown(KeyCode.C)) {
            switchCamera();
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            RC.togglePath();
        }

        updateUI();
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
    }

    void updateUI() {
        UI.setVelocityText(RC.getRobotVelocity());
        UI.setMotorLText(RC.getLeftMotorSpeed());
        UI.setMotorRText(RC.getRightMotorSpeed());
        UI.setSensorText(RC.getSensorReading());
    }
}
