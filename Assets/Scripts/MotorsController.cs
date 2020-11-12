using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorsController : MonoBehaviour
{
    public WheelCollider WC;        // Collider of the wheel
    public GameObject WheelModel;   // Visual model of the wheel

    public float maxSpeed;          // Maximum speed allowed for motor
    private float speed = 0;        // Motor speed

    private float wheelCircumference;   // Wheel circumference in meters
    private float rotation = 0;         // Wheel rotation in degrees

    public int encoderResolution;     // Resolution of the motor encoder. Number of ticks per 1 full rotation.

    // Start is called before the first frame update
    void Start()
    {
        WC = this.GetComponent<WheelCollider>();
        wheelCircumference = 2 * Mathf.PI * WC.radius;
    }

    // Update is called once per frame
    void Update()
    {
        updateWheelModelRotation();
        rotation += (WC.rpm * (Time.deltaTime/60f)) * 360f;
    }


    public void addSpeed(float newSpeed) {
        speed += newSpeed;
        if (speed > maxSpeed) {
            speed = maxSpeed;
        }
        if (speed < -maxSpeed) {
            speed = -maxSpeed;
        }

        WC.motorTorque = speed;
    }

    public void setSpeed(float newSpeed) {
        speed = newSpeed;
        if (speed > maxSpeed) {
            speed = maxSpeed;
        }
        if (speed < -maxSpeed) {
            speed = -maxSpeed;
        }

        WC.motorTorque = speed;
    }

    public void setSpeedPercent(float speedPercent) {
        if (speedPercent > 100) {
            speedPercent = 100;
        }
        if (speedPercent < -100) {
            speedPercent = -100;
        }

        speedPercent /= 100;
        speed = maxSpeed * speedPercent;
        if (speed > maxSpeed) {
            speed = maxSpeed;
        }
        if (speed < -maxSpeed) {
            speed = -maxSpeed;
        }

        WC.motorTorque = speed;
    }

    public void setBrake(bool brake_set) {
        if (brake_set) {
            WC.brakeTorque = Mathf.Infinity;
        } else {
            WC.brakeTorque = 0;
        }
    }

    public float getSpeed() {
        return speed;
    }

    public float getSpeedPercent() {
        return (speed/maxSpeed) * 100f;
    }

    // Returns number of encoder ticks based on rotation and resolution
    public int getRotationTicks() {
        return (int)((rotation/360f) * encoderResolution);
    }

    private float getDistance() {
        return (rotation/360f) * wheelCircumference;
    }

    public void resetTicks() {
        rotation = 0;
    }

    void updateWheelModelRotation() {
        Quaternion rotation;
        Vector3 position;
        WC.GetWorldPose(out position, out rotation);
        rotation = rotation * Quaternion.Euler(new Vector3(0, 0, 90));
        WheelModel.transform.position = position;
        WheelModel.transform.rotation = rotation;
    }
}
