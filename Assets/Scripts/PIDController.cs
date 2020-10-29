using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIDController : MonoBehaviour
{
    public MotorsController motorRight;
    public MotorsController motorLeft;
    public SensorController sensorFront;

    [Header("PD variables for driving stright")]
    public double Kp;
    public double Kd;
    private double u;
    private double e;
    private double e_prev;
    private double distance;
    public double distance_wanted;
    bool arrived;

    [Header("PD variables for rotating")]
    public double Kp_r;
    public double Kd_r;
    private double u_r;
    private double e_r;
    private double e_prev_r;
    private double distance_r;
    [Range(0, 360)]
    public float rotation_wanted;
    private float distance_wanted_r;
    bool arrived_r;

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
        
        e_r = 0;
        e_prev_r = 0;
        distance_r = 0;
        distance_wanted_r = (float)(2f * (rotation_wanted/360f) * Mathf.PI * 0.0525f);
        arrived_r = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > 1.0f) {
            rotate(Side.right);
            //drive();
        }

    }

    void drive() {
        if (sensorFront.getSensorReady()) {
            if (!arrived) {
                distance = sensorImpulseToDistance(sensorFront.getHitTicks());

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

    void rotate(Side side) {
        if (!arrived_r) {
            if (side == Side.right) {
                distance_r = motorImpulseToDistance(motorLeft.getRotationTicks());
            } else {
                distance_r = motorImpulseToDistance(motorRight.getRotationTicks());
            }

            e_r = distance_wanted_r - distance_r;
            u_r = Kp_r*e_r + Kd_r*(e_r - e_prev_r);
            if (u_r > 15f) {
                u_r = 15f;
            }
        } else {
            u_r = 0;
        }

        if (e_r <= 0.001f) {
            arrived_r = true;
        }

        if (u_r <= 0) {
            motorLeft.setBrake(true);
            motorRight.setBrake(true);
            arrived_r = true;
        }

        if (side == Side.right) {
            motorLeft.setSpeedPercent((float)u_r);
            motorRight.setSpeedPercent(-(float)u_r);
        } else if (side == Side.left) {
            motorLeft.setSpeedPercent(-(float)u_r);
            motorRight.setSpeedPercent((float)u_r);
        }
   
        e_prev = e;   
    }

    double motorImpulseToDistance(int impulseCount) {
        return 2 * Mathf.PI * motorLeft.WC.radius * (impulseCount/(double)motorLeft.encoderResolution);
    }

    double sensorImpulseToDistance(int impulseCount) {
        return sensorFront.maxHitDistance * (impulseCount/sensorFront.sensorResolution); 
    }
}
