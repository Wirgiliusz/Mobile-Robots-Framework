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
    private float distance;
    public float distance_wanted;
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
        //distance_wanted_r = (int)(rotation_wanted * (0.0525f/motorLeft.WC.radius)) * (motorLeft.encoderResolution/360);
        distance_wanted_r = (float)(2f * (rotation_wanted/360f) * Mathf.PI * 0.0525f);
        Debug.Log("-> Wanted: " + distance_wanted_r);
        arrived_r = false;
    }

    // ! ------------ 
    // TODO
    // - Zdecydowac czy uzywac impulsow (zmienic czujnik na impulsy) czy dystansu (zmienic kola na dystans)
    // - Sprawdzic dokladniej dzialanie PD 
    // - Zrobic aby robot przejechal labirynt
    // ! ------------ 

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Arrived?: " + arrived_r);
        Debug.Log("e: " + e_r);
        Debug.Log("u: " + u_r);
        Debug.Log("Dist L: " + motorLeft.getDistance());
        Debug.Log("Dist R: " + motorRight.getDistance());

        timer += Time.deltaTime;
        if (timer > 1.0f) {
            rotate(Side.right);
            //drive();
        }

    }

    void drive() {
        if (sensorFront.getSensorReady()) {
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

    void rotate(Side side) {
        if (!arrived_r) {
            if (side == Side.right) {
                //distance_r = motorLeft.getDistance();
                //distance_r = motorLeft.getRotation();
                distance_r = impulseToDistance(motorLeft.getRotation());
            } else {
                //distance_r = motorRight.getDistance();
                //distance_r = motorRight.getRotation();
                distance_r = impulseToDistance(motorRight.getRotation());
            }
            //Debug.Log("Distance: " + distance_r);

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

    double impulseToDistance(int impulseCount) {
        return 2 * Mathf.PI * motorLeft.WC.radius * (impulseCount/motorLeft.encoderResolution);
    }
}
