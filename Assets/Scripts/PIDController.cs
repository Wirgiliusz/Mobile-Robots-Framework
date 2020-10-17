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

    // Start is called before the first frame update
    void Start()
    {
        u = 0;
        e = 0;
        e_prev = 0;
        arrived = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        
    }
}
