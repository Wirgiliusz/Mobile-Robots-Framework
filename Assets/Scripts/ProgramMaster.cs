using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ProgramMaster : MonoBehaviour
{
    public UIController UI;
    private List<GameObject> robotsList;
    private List<GameObject> motorsList;
    private List<GameObject> sensorsList;

    private int cameraIterator = 2; // Index of currently used camera
    public Camera robotCamera;
    public Camera overheadCamera;
    public Camera freeCamera;
    public Vector3 robotCameraPositionOffset;
    public Vector3 robotCameraRotationOffset;
    public Vector3 freeCameraOffset;

    private GameObject selectedRobot = null;
    private GameObject[] robotsArr;

    private double simulationTime;

    // Start is called before the first frame update
    void Start()
    {
        simulationTime = 0;

        robotsArr = GameObject.FindGameObjectsWithTag("Robot");

        foreach (GameObject robot in robotsArr) {
            PIDController PC = robot.GetComponent<PIDController>();
            if (PC != null) {
                robot.GetComponent<PIDController>().enabled = false;
            }
        }
        robotCamera.enabled = false;
        overheadCamera.enabled = false;
        freeCamera.enabled = true;  

        robotsList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Robot"));
        motorsList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Motor"));
        sensorsList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Sensor"));

        UI.createUiElements(robotsList, motorsList, sensorsList);
    }

    // Update is called once per frame
    void Update()
    {
        simulationTime = Time.timeSinceLevelLoad;
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
            togglePaths();
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            turnOnController();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            restartSimulation();
        }

        if (Input.GetMouseButtonDown(0)) {
            checkForRobotSelect();
        }

        updateCameraPosition();
        UI.updateUiElements(robotsArr, simulationTime);
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

    void updateCameraPosition() {
        if (selectedRobot != null) {
            Vector3 robotPos = selectedRobot.transform.GetChild(0).transform.position;
            Vector3 robotRot = selectedRobot.transform.GetChild(0).transform.rotation.eulerAngles;
            
            Vector3 rot = robotRot + robotCameraRotationOffset;
            robotCamera.transform.position = selectedRobot.transform.GetChild(0).transform.position + Quaternion.Euler(rot)*robotCameraPositionOffset;
            robotCamera.transform.rotation = Quaternion.Euler(rot);
        
            overheadCamera.transform.position = robotPos + new Vector3(0,10,0);
            overheadCamera.transform.rotation = Quaternion.Euler(robotRot + new Vector3(90,0,0));

            freeCamera.transform.position = robotPos + freeCameraOffset;
        }
    }

    public void turnOnController() {
        foreach (GameObject robot in robotsArr) {
            PIDController PC = robot.GetComponent<PIDController>();
            if (PC != null) {
                robot.GetComponent<PIDController>().enabled = true;
            }
        }
    }

    public void restartSimulation() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void togglePaths() {
        foreach (GameObject robot in robotsArr) {
            robot.GetComponent<RobotController>().togglePath();
        }
    }

    public void toggleSensorsRays() {
        foreach (GameObject sensor in sensorsList) {
            sensor.GetComponent<SensorController>().toggleSensorRay();
        }
    }

    void checkForRobotSelect() {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
            if (!EventSystem.current.IsPointerOverGameObject()) {   // Check if mouse isn't over UI element
                if (hit.transform.parent != null && hit.transform.parent.gameObject.tag == "Robot") {
                    Debug.Log("Selected");
                    selectedRobot = hit.transform.parent.gameObject;
                } else {
                    Debug.Log("Unselected robot");
                    selectedRobot = null;
                }
            }
        } 
    }

}
