using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RobotController : MonoBehaviour
{
    private MotorController[] motorsControllers;
    private SensorController[] sensorsControllers;

    public TrailRenderer travelPath;
    bool isTravelPathHidden = false;

    private float robotVelocity = 0;

    private Rigidbody robotRb;

    private bool drawPath = false;
    public GameObject pathPoint;
    private float pathPointTimer;
    public float pathPointSpawnTime;
    public int maxPathPointCount;
    private List<GameObject> pathPointsList;
    public float maxVelocityForPath;

    // Start is called before the first frame update
    void Awake() {
        sensorsControllers = GetComponentsInChildren<SensorController>();
        motorsControllers = GetComponentsInChildren<MotorController>();

        robotRb = this.GetComponentInChildren<Rigidbody>();
        pathPointsList = new List<GameObject>();
    }

    void Start()
    {
        pathPointTimer = 0f;
    }

    void FixedUpdate() {
        pathPointTimer += Time.deltaTime;

        if (drawPath) {
            drawVelocityPath();
        } else {
            deleteVelocityPath();
        }
    }

    void Update() {
        robotVelocity = robotRb.velocity.magnitude;
    }


    void drawVelocityPath() {
        if (pathPointTimer >= pathPointSpawnTime && robotVelocity > 0.01f) {
            GameObject newPoint = Instantiate(pathPoint, this.transform.GetChild(0).position, this.transform.GetChild(0).rotation, this.transform);

            float colorHue = 0.33f - (robotVelocity/maxVelocityForPath)*0.33f;
            if (colorHue < 0f) {
                colorHue = 0f;
            }
            newPoint.GetComponent<Renderer>().material.SetColor("_Color", Color.HSVToRGB(colorHue, 1f, 1f));  

            pathPointsList.Add(newPoint);

            if (pathPointsList.Count > maxPathPointCount) {
                Destroy(pathPointsList[0]);
                pathPointsList.RemoveAt(0);
            }
            pathPointTimer = 0f;
        }
    }

    void deleteVelocityPath() {
        if (pathPointsList.Count > 0) {
            foreach (GameObject point in pathPointsList) {
                Destroy(point);
            }
            pathPointsList.Clear();
        }
    }

    public SensorController[] getSensorsControllers() {
        return sensorsControllers;
    }

    public MotorController[] getMotorsControllers() {
        return motorsControllers;
    }

    public float getRobotVelocity() {
        return robotVelocity;
    }

    public void togglePath() {
        if (isTravelPathHidden) {
            travelPath.startColor = new Color(255,255,255,1f);
            travelPath.endColor = new Color(255,255,255,1f);
            isTravelPathHidden = false;
        } else if (!isTravelPathHidden) {
            travelPath.startColor = new Color(0,0,0,0f);
            travelPath.endColor = new Color(0,0,0,0f);
            isTravelPathHidden = true;
        }
    }

    public void toggleVelocityPath() {
        if (drawPath == true) {
            drawPath = false;
        } else {
            drawPath = true;
        }
    }

}
