using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIController : MonoBehaviour
{
    public float textPosSpace;
    public GameObject textObj;
    public GameObject imageBg;

    private List<GameObject> uiTextsList;

    public Text simulationTimeText;

    public Dropdown scenesDropdown;
    private List<String> dropdownOptions;

    // Start is called before the first frame update
    void Awake()
    {
        uiTextsList = new List<GameObject>();
        dropdownOptions = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createUiElements(List<GameObject> robotsList, List<GameObject> motorsList, List<GameObject> sensorsList) {
        float textPos = -40f;

        foreach (GameObject robotObj in robotsList) {
            // Check which elements corespond to which robot
            GameObject robotTextLabel = Instantiate(textObj, this.transform);
            robotTextLabel.GetComponent<Text>().text = "Robot name:";
            robotTextLabel.GetComponent<Text>().fontStyle = FontStyle.Bold;
            robotTextLabel.GetComponent<RectTransform>().anchoredPosition = new Vector3(120f, textPos,  0f);
            textPos -= textPosSpace;

            GameObject robotTextValue = Instantiate(textObj, this.transform);
            robotTextValue.name = robotObj.name + "NameTextValue";
            robotTextValue.GetComponent<Text>().text = robotObj.name;
            robotTextValue.GetComponent<RectTransform>().anchoredPosition = new Vector3(150f, textPos, 0f);
            uiTextsList.Add(robotTextValue);
            textPos -= textPosSpace;

            GameObject velocityTextLabel = Instantiate(textObj, this.transform);
            velocityTextLabel.GetComponent<Text>().text = "Velocity:";
            velocityTextLabel.GetComponent<Text>().fontStyle = FontStyle.Bold;
            velocityTextLabel.GetComponent<RectTransform>().anchoredPosition = new Vector3(120f, textPos,  0f);
            textPos -= textPosSpace;

            GameObject velocityTextValue = Instantiate(textObj, this.transform);
            velocityTextValue.name = robotObj.name + "VelocityTextValue";
            velocityTextValue.GetComponent<Text>().text = robotObj.name;
            velocityTextValue.GetComponent<RectTransform>().anchoredPosition = new Vector3(150f, textPos, 0f);
            uiTextsList.Add(velocityTextValue);
            textPos -= textPosSpace;

            GameObject motorsTextLabel = Instantiate(textObj, this.transform);
            motorsTextLabel.GetComponent<Text>().text = "Motors:";
            motorsTextLabel.GetComponent<Text>().fontStyle = FontStyle.Bold;
            motorsTextLabel.GetComponent<RectTransform>().anchoredPosition = new Vector3(120f, textPos,  0f);
            textPos -= textPosSpace;
            foreach (GameObject motorObj in motorsList) {
                if (motorObj.transform.root.gameObject.name == robotObj.name) {
                    // Create UI element for motor readings for current robot
                    GameObject motorTextValue = Instantiate(textObj, this.transform);
                    motorTextValue.name = robotObj.name + motorObj.transform.parent.name + "TextValue";
                    motorTextValue.GetComponent<Text>().text = motorObj.transform.parent.name;
                    motorTextValue.GetComponent<RectTransform>().anchoredPosition = new Vector3(150f, textPos, 0f);
                    uiTextsList.Add(motorTextValue);
                    textPos -= textPosSpace;
                }
            }

            GameObject sensorsTextLabel = Instantiate(textObj, this.transform);
            sensorsTextLabel.GetComponent<Text>().text = "Sensors:";
            sensorsTextLabel.GetComponent<Text>().fontStyle = FontStyle.Bold;
            sensorsTextLabel.GetComponent<RectTransform>().anchoredPosition = new Vector3(120f, textPos,  0f);
            textPos -= textPosSpace;
            foreach (GameObject sensorObj in sensorsList) {
                if (sensorObj.transform.root.gameObject.name == robotObj.name) {
                    // Create UI element for sensor readings for current robot
                    GameObject sensorTextValue = Instantiate(textObj, this.transform);
                    sensorTextValue.name = robotObj.name + sensorObj.name + "TextValue";
                    sensorTextValue.GetComponent<Text>().text = sensorObj.name;
                    sensorTextValue.GetComponent<RectTransform>().anchoredPosition = new Vector3(150f, textPos, 0f);
                    uiTextsList.Add(sensorTextValue);
                    textPos -= textPosSpace;
                }
            }
            textPos -= textPosSpace;
        }

        GameObject imageBackground = Instantiate(imageBg, this.transform);
        imageBackground.transform.SetAsFirstSibling();
        imageBackground.GetComponent<RectTransform>().anchoredPosition = new Vector3(150f, textPos/2,  0f);
        imageBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(280f, -(textPos + 40f));

        dropdownOptions.Add(SceneManager.GetActiveScene().name);
        for (int i=0; i<UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings; ++i) {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
            if (!dropdownOptions.Contains(sceneName)) {
                dropdownOptions.Add(sceneName);
            }

        }
        scenesDropdown.ClearOptions();
        scenesDropdown.AddOptions(dropdownOptions);
    }

    public void updateUiElements(GameObject[] robotsArr, double simulationTime) {
        foreach (GameObject robotObj in robotsArr) { // Choose robot
            MotorController[] motorsArr = robotObj.GetComponentsInChildren<MotorController>();
            SensorController[] sensorsArr = robotObj.GetComponentsInChildren<SensorController>();

            foreach (GameObject textObj in uiTextsList) { // For every ui text object that should be updated
                if (textObj.name.Contains(robotObj.name)) { // Check if ui text objects has name that contains choosen robot name 
                    foreach (MotorController motorObj in motorsArr) {
                        if (textObj.name.Contains(motorObj.transform.parent.name)) {
                            textObj.GetComponent<Text>().text = motorObj.transform.parent.name + ": " + String.Format("{0:0.00}", motorObj.getSpeedPercent()) + "%";
                        }
                    }

                    foreach (SensorController sensorObj in sensorsArr) {
                        if (textObj.name.Contains(sensorObj.name)) {
                            textObj.GetComponent<Text>().text = sensorObj.name + ": " + String.Format("{0:0.00}", sensorObj.getHitDistance()) + "m";
                        }
                    }

                    if (textObj.name.Contains("Velocity")) {
                        textObj.GetComponent<Text>().text = "V: " + String.Format("{0:0.00}", robotObj.GetComponent<RobotController>().getRobotVelocity()) + "m/s";
                    }
                }
            }
        }

        simulationTimeText.text = String.Format("{0:0.00}", simulationTime) + "s";
    }

    public void changeScene() {
        SceneManager.LoadScene(scenesDropdown.options[scenesDropdown.value].text);
    }
}
