using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    public Text velocity;
    public Text motorL;
    public Text motorR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setVelocityText(float v) {
        velocity.text = "Velocity\n" +  String.Format("{0:0.00}", v) + " m/s";
    }

    public void setMotorLText(float s) {
        motorL.text = "Motor L\n" + String.Format("{0:0.00}", s);
    }

    public void setMotorRText(float s) {
        motorR.text = "Motor R\n" + String.Format("{0:0.00}", s);
    }
}
