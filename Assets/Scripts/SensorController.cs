using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorController : MonoBehaviour
{
    public float maxHitDistance;
    public float minHitDistance;
    private LineRenderer sensorLine;
    private float hitDistance;  
    bool sensorReady;

    public float sensorResolution; // Resolution of the sensor. Number of ticks at maxHitDistance

    // Start is called before the first frame update
    void Start()
    {
        sensorLine = this.GetComponent<LineRenderer>();
        sensorLine.SetPosition(1, Vector3.forward * maxHitDistance * 50f);
        sensorReady = false;
        hitDistance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(downRay, out hit) && hit.distance <= maxHitDistance) {
            if (hit.distance >= minHitDistance) {
                hitDistance = hit.distance;
                sensorLine.startColor = Color.red;
                sensorLine.endColor = Color.red;
            } else {
                hitDistance = minHitDistance;
                sensorLine.startColor = Color.yellow;
                sensorLine.endColor = Color.yellow;
            }
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

    public int getHitTicks() {
        return (int)((hitDistance/maxHitDistance) * sensorResolution);
    }

    public bool getSensorReady() {
        return sensorReady;
    }

    public void toggleSensorRay() {
        if (sensorLine.enabled == true) {
            sensorLine.enabled = false;
        } else if (sensorLine.enabled == false) {
            sensorLine.enabled = true;
        }
    }
}
