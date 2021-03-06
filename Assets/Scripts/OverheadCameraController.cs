﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadCameraController : MonoBehaviour
{
    public float flySpeed;
    public float fastMultiplier;
    public float slowMultiplier;
    
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.enabled) {
            /* Overhead camera control keybinds
            *   Q/E:    move down/up    [QE]
            */
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

            if (Input.GetAxis("Horizontal") != 0 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) {
                transform.Translate(Vector3.right * flySpeed * Input.GetAxis("Horizontal"));
            }
            if (Input.GetAxis("Vertical") != 0 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))) {
                transform.Translate(Vector3.up * flySpeed * Input.GetAxis("Vertical"));
            }

            if (Input.GetKey(KeyCode.Q)) {
                cam.orthographicSize -= flySpeed;
            }
            if (Input.GetKey(KeyCode.E)) {
                cam.orthographicSize += flySpeed;
            }
        }
    }
}
