﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    public float flySpeed;
    public float fastMultiplier;
    public float slowMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            flySpeed *= fastMultiplier;
        } 
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            flySpeed /= fastMultiplier;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            flySpeed *= slowMultiplier;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl)) {
            flySpeed /= slowMultiplier;
        }

        if (Input.GetAxis("Horizontal") != 0) {
            transform.Translate(Vector3.right * flySpeed * Input.GetAxis("Horizontal"));
        }
        if (Input.GetAxis("Vertical") != 0) {
            transform.Translate(Vector3.forward * flySpeed * Input.GetAxis("Vertical"));
        }

        if (Input.GetKey(KeyCode.Q)) {
            transform.Translate(Vector3.down * flySpeed);
        }
        if (Input.GetKey(KeyCode.E)) {
            transform.Translate(Vector3.up * flySpeed);
        }
    }
}
