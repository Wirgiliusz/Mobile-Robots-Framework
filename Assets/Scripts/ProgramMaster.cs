using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgramMaster : MonoBehaviour
{
    public UIController UI;

    private int cameraIterator = 0; // Index of currently used camera
    public Camera robotCamera;
    public Camera overheadCamera;
    public Camera freeCamera;

    public GameObject Robot;
    private RobotController RC;
    private PIDController PC;

    // Start is called before the first frame update
    void Start()
    {
        RC = Robot.GetComponent<RobotController>();
        PC = Robot.GetComponent<PIDController>();
        overheadCamera.enabled = false;
        freeCamera.enabled = false;  
    }

    // Update is called once per frame
    void Update()
    {
        /* Global program keybinds
        * C:    Switch camera       [C]
        * T:    Toggle travel path  [T]
        * P:    Play simulation     [P]
        * R:    Restart simulation  [R]
        */
        if (Input.GetKeyDown(KeyCode.C)) {
            switchCamera();
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            RC.togglePath();
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            PC.setPlay(true);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
                if (hit.transform.parent != null && hit.transform.parent.gameObject.tag == "Robot") {
                    Debug.Log("Clicked on robot");
                } else {
                    Debug.Log("Missed robot");
                }
            } else {
                Debug.Log("No raycast hit");
            }

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
