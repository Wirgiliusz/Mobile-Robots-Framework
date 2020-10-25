using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorController : MonoBehaviour
{
    public float maxHitDistance;
    private LineRenderer sensorLine;
    private float hitDistance;  
    bool sensorReady;

    // Start is called before the first frame update
    void Start()
    {
        sensorLine = this.GetComponent<LineRenderer>();
        sensorLine.SetPosition(1, transform.forward * maxHitDistance * 66.666f);
        sensorReady = false;
        hitDistance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(downRay, out hit) && hit.distance <= maxHitDistance) {
            hitDistance = hit.distance;
            sensorLine.startColor = Color.red;
            sensorLine.endColor = Color.red;
        } else {
            hitDistance = maxHitDistance;
            sensorLine.startColor = Color.yellow;
            sensorLine.endColor = Color.yellow;
        }
        
        sensorReady = true;
    }

    public float getHitDistance() {
        return hitDistance;
    }

    public bool getSensorReady() {
        return sensorReady;
    }
}
