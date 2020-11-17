using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    public float textPosSpace;
    public GameObject textObj;

    private List<GameObject> uiTextsList;

    // Start is called before the first frame update
    void Start()
    {
        uiTextsList = new List<GameObject>();
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

            GameObject motorsTextLabel = Instantiate(textObj, this.transform);
            motorsTextLabel.GetComponent<Text>().text = "Motors:";
            motorsTextLabel.GetComponent<Text>().fontStyle = FontStyle.Bold;
            motorsTextLabel.GetComponent<RectTransform>().anchoredPosition = new Vector3(120f, textPos,  0f);
            textPos -= textPosSpace;
            foreach (GameObject motorObj in motorsList) {
                if (motorObj.transform.parent.parent.parent.name == robotObj.name) {
                    // Create UI element for motor readings for current robot
                    GameObject motorTextValue = Instantiate(textObj, this.transform);
                    motorTextValue.name = robotObj.name + motorObj.name + "TextValue";
                    motorTextValue.GetComponent<Text>().text = motorObj.name;
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
                if (sensorObj.transform.parent.parent.parent.name == robotObj.name) {
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
    }

    public void updateUiElements(GameObject[] robotsArr) {
        foreach (GameObject robotObj in robotsArr) { // Choose robot
            MotorsController[] motorsArr = robotObj.GetComponentsInChildren<MotorsController>();
            SensorController[] sensorsArr = robotObj.GetComponentsInChildren<SensorController>();

            foreach (GameObject textObj in uiTextsList) { // For every ui text object that should be updated
                if (textObj.name.Contains(robotObj.name)) { // Check if ui text objects has name that contains choosen robot name 
                    foreach (MotorsController motorObj in motorsArr) {
                        if (textObj.name.Contains(motorObj.name)) {
                            textObj.GetComponent<Text>().text = motorObj.name + ": " + String.Format("{0:0.00}", motorObj.getSpeedPercent()) + "%";
                        }
                    }

                    foreach (SensorController sensorObj in sensorsArr) {
                        if (textObj.name.Contains(sensorObj.name)) {
                            textObj.GetComponent<Text>().text = sensorObj.name + ": " + String.Format("{0:0.00}", sensorObj.getHitDistance()) + "m";
                        }
                    }


                }
            }
        }
    }
}
