using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    public float textPosSpace;
    public GameObject textObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createUiElements(List<GameObject> robotsList, List<GameObject> motorsList, List<GameObject> sensorsList) {
        float textPos = -40f;
        int createdTexts = 0;

        foreach (GameObject robotObj in robotsList) {
            // Check which elements corespond to which robot
            GameObject robotTextLabel = Instantiate(textObj, this.transform);
            robotTextLabel.GetComponent<Text>().text = "Robot name:";
            robotTextLabel.GetComponent<Text>().fontStyle = FontStyle.Bold;
            robotTextLabel.GetComponent<RectTransform>().anchoredPosition = new Vector3(120f, textPos,  0f);
            textPos -= textPosSpace;
            createdTexts++;

            GameObject robotTextValue = Instantiate(textObj, this.transform);
            robotTextValue.GetComponent<Text>().text = robotObj.name;
            robotTextValue.GetComponent<RectTransform>().anchoredPosition = new Vector3(150f, textPos, 0f);
            textPos -= textPosSpace;
            createdTexts++;

            GameObject motorsTextLabel = Instantiate(textObj, this.transform);
            motorsTextLabel.GetComponent<Text>().text = "Motors:";
            motorsTextLabel.GetComponent<Text>().fontStyle = FontStyle.Bold;
            motorsTextLabel.GetComponent<RectTransform>().anchoredPosition = new Vector3(120f, textPos,  0f);
            textPos -= textPosSpace;
            createdTexts++;
            foreach (GameObject motorObj in motorsList) {
                if (motorObj.transform.parent.parent.parent.name == robotObj.name) {
                    // Create UI element for motor readings for current robot
                    GameObject motorTextValue = Instantiate(textObj, this.transform);
                    motorTextValue.GetComponent<Text>().text = motorObj.name;
                    motorTextValue.GetComponent<RectTransform>().anchoredPosition = new Vector3(150f, textPos, 0f);
                    textPos -= textPosSpace;
                    createdTexts++;
                }
            }

            GameObject sensorsTextLabel = Instantiate(textObj, this.transform);
            sensorsTextLabel.GetComponent<Text>().text = "Sensors:";
            sensorsTextLabel.GetComponent<Text>().fontStyle = FontStyle.Bold;
            sensorsTextLabel.GetComponent<RectTransform>().anchoredPosition = new Vector3(120f, textPos,  0f);
            textPos -= textPosSpace;
            createdTexts++;
            foreach (GameObject sensorObj in sensorsList) {
                if (sensorObj.transform.parent.parent.parent.name == robotObj.name) {
                    // Create UI element for sensor readings for current robot
                    GameObject sensorTextValue = Instantiate(textObj, this.transform);
                    sensorTextValue.GetComponent<Text>().text = sensorObj.name;
                    sensorTextValue.GetComponent<RectTransform>().anchoredPosition = new Vector3(150f, textPos, 0f);
                    textPos -= textPosSpace;
                    createdTexts++;
                }
            }
            textPos -= textPosSpace;
        }
    }
}
