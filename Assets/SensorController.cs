using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorController : MonoBehaviour
{
    private float hitDistance = -1;   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(downRay, out hit)) {
            hitDistance = hit.distance;
        } else {
            hitDistance = -1f;
        }

        Debug.Log(string.Format("Sensor:  {0:0.00} ", hitDistance));
    }
}
