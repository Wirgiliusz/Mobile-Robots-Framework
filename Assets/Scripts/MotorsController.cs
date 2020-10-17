using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorsController : MonoBehaviour
{
    public WheelCollider WC;        // Collider of the wheel
    public GameObject WheelModel;   // Visual model of the wheel

    public float maxSpeed;
    private float speed = 0;        // Wheel speed

    private float wheelCircumference;
    private float rotation = 0;
    private float distance = 0;

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
        /*
        rotation += ((wheelCircumference * WC.rpm)/360f)/0.75f;
        distance = (rotation * wheelCircumference)/2000f;
        Debug.Log(distance);
        */

        //distance = WC.rpm/Time.deltaTime;
        //rotation += (distance/wheelCircumference) / 360f;

        rotation += (WC.rpm * (Time.deltaTime/60f)) * 360f;
        distance = (rotation/360f) * wheelCircumference;
        Debug.Log("Dist:" + distance + " | Rot: " + rotation);
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
        if (speedPercent < 0) {
            speedPercent = 0;
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

    void updateWheelModelRotation() {
        Quaternion rotation;
        Vector3 position;
        WC.GetWorldPose(out position, out rotation);
        rotation = rotation * Quaternion.Euler(new Vector3(0, 0, 90));
        WheelModel.transform.position = position;
        WheelModel.transform.rotation = rotation;
    }
}
