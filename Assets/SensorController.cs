using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorController : MonoBehaviour
{
    public float maxHitDistance;
    public LineRenderer sensorLine;
    private float hitDistance = -1f;   

    // Start is called before the first frame update
    void Start()
    {
        sensorLine.SetPosition(1, transform.forward * maxHitDistance);
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
            hitDistance = -1f;
            sensorLine.startColor = Color.yellow;
            sensorLine.endColor = Color.yellow;
        }
    }

    public float getHitDistance() {
        return hitDistance;
    }
}
