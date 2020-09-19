using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorsController : MonoBehaviour
{
    public WheelCollider WC;
    public GameObject WheelModel;

    private float speed = 0;   

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


    public void addSpeed(float new_speed) {
        speed += new_speed;
        addSpeedToWheel();
    }

    public void setSpeed(float new_speed) {
        speed = new_speed;
        addSpeedToWheel();
    }

    public void setBrake(bool state) {
        if (state) {
            WC.brakeTorque = Mathf.Infinity;
        } else {
            WC.brakeTorque = 0;
        }
    }

    public float getSpeed() {
        return speed;
    }

    void addSpeedToWheel() {
        WC.motorTorque = speed;

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
