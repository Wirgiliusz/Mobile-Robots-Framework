using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadCameraController : MonoBehaviour
{
    public float zoomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* Free camera control keybinds
        *   Q/E:    move down/up    [QE]
        */
        if (Input.GetKey(KeyCode.Q)) {
            GetComponent<Camera>().orthographicSize -= zoomSpeed;
        }
        if (Input.GetKey(KeyCode.E)) {
            GetComponent<Camera>().orthographicSize += zoomSpeed;
        }
    }
}
