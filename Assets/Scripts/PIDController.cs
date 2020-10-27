using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIDController : MonoBehaviour
{
    public MotorsController motorRight;
    public MotorsController motorLeft;
    public SensorController sensorFront;

    public double Kp;
    public double Kd;
    private double u;
    private double e;
    private double e_prev;
    private float distance;
    public float distance_wanted;
    bool arrived;

    double Kp_r = 0.005f;
    double Kd_r = 0.03f;
    double u_r = 0;
    int e_r = 0;
    int e_prev_r = 0;
    int distance_r = 0;
    int distance_wanted_r;
    bool arrived_r = false;

    float timer = 0;

    enum Side
    {
        right,
        left,
    }

    // Start is called before the first frame update
    void Start()
    {
        u = 0;
        e = 0;
        e_prev = 0;
        arrived = false;
        //rotate();

        distance_wanted_r = (int)(90f * (0.0525f/motorLeft.WC.radius));
        Debug.Log("wanted: " + distance_wanted_r);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        /*
        if (sensorFront.getSensorReady()) {
            //Debug.Log("Arrived: " + arrived + " | u = " + u + " | e = " + e);

            if (!arrived) {
                distance = sensorFront.getHitDistance();

                e = distance - distance_wanted;
                u = Kp*e + Kd*(e - e_prev);
            } else {
                u = 0;
            }
            
            if (u <= 0) {
                motorLeft.setBrake(true);
                motorRight.setBrake(true);
                arrived = true;
            } else {
                motorLeft.setBrake(false);
                motorRight.setBrake(false);
            }

            motorLeft.setSpeedPercent((float)u);
            motorRight.setSpeedPercent((float)u);
        
            e_prev = e;
        }
        */

        if (timer > 1.0f) {
            if (!arrived_r) {
            Debug.Log(distance_r);
            distance_r = motorLeft.getRotation();

            e_r = distance_wanted_r - distance_r;
            Debug.Log("e: " + e_r);
            u_r = Kp_r*e_r + Kd_r*(e_r - e_prev_r);
            Debug.Log("u: " + u_r);

            } else {
                u_r = 0;
            }

            if (u_r <= 0) {
                Debug.Log("how");
                motorLeft.setBrake(true);
                motorRight.setBrake(true);
                arrived_r = true;
            }

            motorLeft.setSpeedPercent((float)u_r);
            motorRight.setSpeedPercent(-(float)u_r);
        
            e_prev = e;
        }
        
    }

    // TODO 
    /*
    void rotate() {
        double Kp_r = 0.05f;
        double Kd_r = 0.3f;
        double u_r = 0;
        double e_r = 0;
        double e_prev_r = 0;
        float distance_r = 0;
        float distance_wanted_r = 90 * (0.1f/motorLeft.WC.radius);
        Debug.Log(distance_wanted_r);
        bool arrived_r = false;

        while(!arrived_r) {
            Debug.Log(distance_r);

            if (!arrived_r) {
                distance_r = motorLeft.getRotation();

                e_r = distance_wanted_r - distance_r;
                u_r = Kp_r*e_r + Kd_r*(e_r - e_prev_r);
            } else {
                u_r = 0;
            }

            if (u <= 0) {
                motorLeft.setBrake(true);
                motorRight.setBrake(true);
                arrived_r = true;
            }

            motorLeft.setSpeedPercent((float)u_r);
            motorRight.setSpeedPercent(-(float)u_r);
        
            e_prev = e;
        }
 
    }
    */
}
