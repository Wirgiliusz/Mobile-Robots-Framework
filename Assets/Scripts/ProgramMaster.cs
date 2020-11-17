using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ProgramMaster : MonoBehaviour
{
    public UIController UI;
    public GameObject uiObj;
    public GameObject textObj;
    private List<GameObject> robotsList;
    private List<GameObject> motorsList;
    private List<GameObject> sensorsList;

    private int cameraIterator = 0; // Index of currently used camera
    public Camera robotCamera;
    public Camera overheadCamera;
    public Camera freeCamera;
    public Vector3 robotCameraPositionOffset;
    public Vector3 robotCameraRotationOffset;
    public Vector3 freeCameraOffset;

    private GameObject selectedRobot = null;

    public GameObject Robot;
    private RobotController RC;
    private PIDController PC;

    // Start is called before the first frame update
    void Start()
    {
        RC = Robot.GetComponent<RobotController>();
        PC = Robot.GetComponent<PIDController>();
        PC.enabled = false;
        overheadCamera.enabled = false;
        freeCamera.enabled = false;  

        robotsList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Robot"));
        motorsList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Motor"));
        sensorsList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Sensor"));

        createUiElements();
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
            turnOnController();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            restartSimulation();
        }

        if (Input.GetMouseButtonDown(0)) {
            checkForRobotSelect();
        }

        updateCameraPosition();
        //updateUI();
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

    void updateUI() {
        UI.setVelocityText(RC.getRobotVelocity());
        UI.setMotorLText(RC.getLeftMotorSpeedPercent());
        //UI.setMotorRText(RC.getRightMotorSpeedPercent());
        UI.setSensorText(RC.getSensorReading());
    }

    public void turnOnController() {
        PC.enabled = true;
    }

    public void restartSimulation() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    void createUiElements() {
        float posRobotLabel = -70f;
        float posMotorLabel = -190f;
        float posSensorLabel = 0f;
        int createdTexts = 0;

        foreach (GameObject robotObj in robotsList) {
            // Check which elements corespond to which robot
            GameObject robotText = Instantiate(textObj, uiObj.transform);
            robotText.GetComponent<RectTransform>().anchoredPosition = new Vector3(150f, posRobotLabel, 0f);
            createdTexts++;

            foreach (GameObject motorObj in motorsList) {
                if (motorObj.transform.parent.parent.parent.tag == robotObj.tag) {
                    // Create UI element for motor readings for current robot
                    GameObject motorText = Instantiate(textObj, uiObj.transform);
                    motorText.GetComponent<RectTransform>().anchoredPosition = new Vector3(150f, posMotorLabel, 0f);

                    createdTexts++;

                    posMotorLabel -= 30f;
                }
            }

            posSensorLabel = posMotorLabel - 30f;
            foreach (GameObject sensorObj in sensorsList) {
                if (sensorObj.transform.parent.parent.parent.tag == robotObj.tag) {
                    // Create UI element for sensor readings for current robot
                    GameObject sensorText = Instantiate(textObj, uiObj.transform);
                    sensorText.GetComponent<RectTransform>().anchoredPosition = new Vector3(150f, posSensorLabel, 0f);
                    createdTexts++;

                    posSensorLabel -= 30f;
                }
            }

            posRobotLabel -= 30f*createdTexts - 60f;
        }
        
    }
}
