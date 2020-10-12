using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorsController : MonoBehaviour
{
    public WheelCollider WC;        // Collider of the wheel
    public GameObject WheelModel;   // Visual model of the wheel

    public float maxSpeed;
    private float speed = 0;        // Wheel speed
    

    // Start is called before the first frame update
    void Start()
    {
        WC = this.GetComponent<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        updateWheelModelRotation();
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
        /*
        if (speedPercent > 0 && speedPercent <= 1) {
            speedPercent = 1f;
        }
        */
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
