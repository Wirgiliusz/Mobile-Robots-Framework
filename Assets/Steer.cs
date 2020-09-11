using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steer : MonoBehaviour
{
    public WheelCollider WC;
    public float maxSteerAngle = 30;

    // Start is called before the first frame update
    void Start()
    {
        WC = this.GetComponent<WheelCollider>();
    }

    void steering(float steer) {
        steer = Mathf.Clamp(steer, -1, 1) * maxSteerAngle;
        WC.steerAngle = steer;
    }

    // Update is called once per frame
    void Update()
    {
        float steer = Input.GetAxis("Horizontal");
        steering(steer);
    }
}
