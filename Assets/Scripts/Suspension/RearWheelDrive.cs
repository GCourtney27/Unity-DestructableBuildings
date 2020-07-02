using UnityEngine;
using System.Collections;
using System;
using UnityEngine.AI;

public class RearWheelDrive : MonoBehaviour
{

    private WheelCollider[] wheels;

    public float maxTorque = 300;

    public float idleRotationSpeed = 1.0f;

    bool isPlayer = false;
    public void Start()
    {
        AssignWheelMesh();
        if (this.tag == "Player")
            isPlayer = true;
        else
            isPlayer = false;
    }

    public void Update()
    {
        if (isPlayer)
            UpdatePlayerWheels();
        else
            UpdateEnemyWheels();
    }

    void AssignWheelMesh()
    {
        wheels = GetComponentsInChildren<WheelCollider>();
    }

    void UpdateEnemyWheels()
    {
        float speed = maxTorque;
        if(!GetComponent<WaypointAI>().reachedGoal)
        {
            foreach (WheelCollider wheel in wheels)
            {
                wheel.motorTorque = speed;
                UpdateWheelMesh(wheel);
            }
        }
        else
        {
            foreach (WheelCollider wheel in wheels)
            {
                wheel.motorTorque = 0.0f;
                UpdateWheelMesh(wheel);
            }
        }
    }

    void UpdatePlayerWheels()
    {
        float torqueFB = maxTorque * Input.GetAxis("Vertical");// W, S
        float torqueLR = maxTorque * Input.GetAxis("Horizontal");// A, D

        // If D pressed only move Left wheels
        // If A pressed only move Right wheels
        foreach (WheelCollider wheel in wheels)
        {

            /*
             * Left Wheels are: -1 on X
             * Right Wheels are: 1 on X
             */



            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) // Forward to Left
            {
                if (wheel.transform.localPosition.x == 1)
                {
                    wheel.motorTorque = torqueFB * 2.5f;
                }
                if (wheel.transform.localPosition.x == -1)
                {
                    wheel.motorTorque = torqueLR * 1.5f;
                    //wheel.motorTorque = 0.0f;
                }
                UpdateWheelMesh(wheel);
                continue;
            }
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W)) // Forward to Right
            {
                if (wheel.transform.localPosition.x == -1)
                {
                    wheel.motorTorque = torqueFB * 2.5f;
                }
                if (wheel.transform.localPosition.x == 1)
                {
                    wheel.motorTorque = -torqueLR * 1.5f;
                }
                UpdateWheelMesh(wheel);
                continue;
            }
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S)) // Back to Right
            {
                if (wheel.transform.localPosition.x == 1)
                {
                    wheel.motorTorque = torqueFB * 1.5f;
                }
                if (wheel.transform.localPosition.x == -1)
                {
                    wheel.motorTorque = torqueLR * 2.5f;
                }
                UpdateWheelMesh(wheel);
                continue;
            }

            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S)) // Back to Left
            {
                if (wheel.transform.localPosition.x == -1)
                {
                    wheel.motorTorque = torqueLR * 1.5f;
                }
                if (wheel.transform.localPosition.x == 1)
                {
                    wheel.motorTorque = torqueFB * 2.5f;
                }
                UpdateWheelMesh(wheel);
                continue;
            }

            if (Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.A)) // Idle Left
            {
                if (wheel.transform.localPosition.x == 1)
                {
                    wheel.motorTorque = torqueLR * idleRotationSpeed;
                }
                if (wheel.transform.localPosition.x == -1)
                {
                    wheel.motorTorque = 0.0f;// torqueLR * idleRotationSpeed;
                }
            }
            if (Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.D)) // Idle Right
            {
                if (wheel.transform.localPosition.x == -1)
                {
                    wheel.motorTorque = torqueLR * idleRotationSpeed;
                }
                if (wheel.transform.localPosition.x == 1)
                {
                    wheel.motorTorque = -torqueLR * idleRotationSpeed;
                }
            }

            if (Input.GetKey(KeyCode.W)) // Forward
            {
                wheel.motorTorque = torqueFB;
            }

            if (Input.GetKey(KeyCode.S)) // Backward
            {
                wheel.motorTorque = torqueFB;
            }

           

            //if (wheel.transform.localPosition.z < 0)
            //    wheel.motorTorque = torque;

            //wheel.motorTorque = torqueFB; // Apply the same amount of torque on all wheels
            // Fetch values and apply to static wheels on front of tank to make them move at the same speed as the rest of the wheels?
            UpdateWheelMesh(wheel);
        }
        
    }
    public float yOffset = 1.0f;
    void UpdateWheelMesh(WheelCollider wheel)
    {

        Quaternion q;
        Vector3 p;
        wheel.GetWorldPose(out p, out q);

        // assume that the only child of the wheelcollider is the wheel shape
        Transform shapeTransform = wheel.transform.GetChild(0);
        shapeTransform.position = p;
        shapeTransform.rotation = q;

        shapeTransform.position = shapeTransform.position + new Vector3(0.0f, yOffset, 0.0f);

        // update wheel for track visuals
        Transform shapeTransform1 = wheel.transform.GetChild(1);
        shapeTransform1.position = shapeTransform.position;
        //shapeTransform1.rotation = q;


    }
}
