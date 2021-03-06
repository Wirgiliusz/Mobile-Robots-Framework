﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIDController : MonoBehaviour
{
    private RobotController robotController;

    private MotorController motorRight;
    private MotorController motorLeft;
    private SensorController sensorFront;

    [Header("PD variables for driving stright")]
    public double Kp;
    public double Kd;
    public double e_expected;
    private double u;
    private double e;
    private double e_prev;
    private double distance;
    public double distance_wanted;
    bool arrived;

    [Header("PD variables for rotating")]
    public float halfDistanceBetweenWheels;
    public double Kp_r;
    public double Kd_r;
    public double e_r_expected;
    private double u_r;
    private double e_r;
    private double e_prev_r;
    private double distance_r;
    [Range(-360, 360)]
    public double rotation_wanted;
    private double distance_wanted_r;
    bool arrived_r;

    enum Side {right, left, }

    enum Direction {driveStright, rotateRight, rotateLeft, stop}

    Direction[] movList = new Direction[] {Direction.driveStright, Direction.rotateRight, Direction.driveStright, Direction.rotateRight, Direction.driveStright, Direction.rotateLeft, Direction.driveStright, Direction.rotateLeft, Direction.driveStright, Direction.stop, };
    int movNumber = 0;
    bool movFinished = false;

    public bool onlyStright;
    public bool onlyRotate;

    // Start is called before the first frame update
    void Start()
    {
        if (onlyStright) {
            movList = new Direction[] {Direction.driveStright, Direction.stop, };
        }
        if (onlyRotate) {
            if (rotation_wanted < 0) {
                movList = new Direction[] {Direction.rotateLeft, Direction.stop, };
                rotation_wanted *= -1;
            } else {
                movList = new Direction[] {Direction.rotateRight, Direction.stop, };
            }
        }
        if (onlyStright && onlyRotate) {
            movList = new Direction[] {Direction.stop, };
        }

        robotController = GetComponent<RobotController>();
        sensorFront = robotController.getSensorsControllers()[0];
        motorLeft = robotController.getMotorsControllers()[0];
        motorRight = robotController.getMotorsControllers()[1];

        u = 0;
        e = 0;
        e_prev = 0;
        arrived = false;
        
        e_r = 0;
        e_prev_r = 0;
        distance_r = 0;
        distance_wanted_r = (float)(2f * (rotation_wanted/360f) * Mathf.PI * halfDistanceBetweenWheels);
        arrived_r = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(movList[movNumber]) {
            case Direction.driveStright:
            drive();
            break;

            case Direction.rotateLeft:
            rotate(Side.left);
            break;

            case Direction.rotateRight:
            rotate(Side.right);
            break;

            case Direction.stop:
            break;

            default:
            Debug.Log("Switch out of bounds!");
            break;
        }

        if (movFinished) {
            resetDrive();
            resetRotate();

            if (movNumber < movList.Length - 1) {
                ++movNumber;
            }

            movFinished = false;
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
                movFinished = true;
            }
            
            if (e <= e_expected && e >= -e_expected) {
                arrived = true;
            }

            if (u == 0) {
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
            movFinished = true;
        }

        if (e_r <= e_r_expected && e_r >= -e_r_expected) {
            arrived_r = true;
        }

        if (u_r == 0) {
            motorLeft.setBrake(true);
            motorRight.setBrake(true);
            arrived_r = true;
        } else {
            motorLeft.setBrake(false);
            motorRight.setBrake(false);
        }

        if (side == Side.right) {
            motorLeft.setSpeedPercent((float)u_r);
            motorRight.setSpeedPercent(-(float)u_r);
        } else if (side == Side.left) {
            motorLeft.setSpeedPercent(-(float)u_r);
            motorRight.setSpeedPercent((float)u_r);
        }
   
        e_prev_r = e_r;   
    }

    void resetDrive() {
        u = 0;
        e = 0;
        e_prev = 0;
        arrived = false;
        motorLeft.resetTicks();
        motorRight.resetTicks();
    }

    void resetRotate() {
        e_r = 0;
        e_prev_r = 0;
        distance_r = 0;
        distance_wanted_r = (float)(2f * (rotation_wanted/360f) * Mathf.PI * 0.0525f);
        arrived_r = false;
    }

    double motorImpulseToDistance(int impulseCount) {
        return 2 * Mathf.PI * motorLeft.WC.radius * (impulseCount/(double)motorLeft.encoderResolution);
    }

    double sensorImpulseToDistance(int impulseCount) {
        return sensorFront.minHitDistance + ((sensorFront.maxHitDistance-sensorFront.minHitDistance) * (impulseCount/sensorFront.sensorResolution)); 
    }

}
