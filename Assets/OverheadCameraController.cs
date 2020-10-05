using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadCameraController : MonoBehaviour
{
    public float zoomSpeed;
    
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
            if (Input.GetKey(KeyCode.Q)) {
                cam.orthographicSize -= zoomSpeed;
            }
            if (Input.GetKey(KeyCode.E)) {
                cam.orthographicSize += zoomSpeed;
            }
        }
    }
}
