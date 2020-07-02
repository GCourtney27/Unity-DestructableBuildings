using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankWheelScript : MonoBehaviour
{

    [SerializeField] [Range(0.0f, 10.0f)] float m_rotationSpeed = 0.0f;

    public WheelCollider wheel_L1_Collider;
    public WheelCollider wheel_L2_Collider;
    public WheelCollider wheel_L3_Collider;
    public WheelCollider wheel_L4_Collider;
    public WheelCollider wheel_L5_Collider;
    public WheelCollider wheel_L6_Collider;
    public WheelCollider wheel_L7_Collider;
    public WheelCollider wheel_L8_Collider;
    public WheelCollider wheel_L9_Collider;
    public WheelCollider wheel_L10_Collider;

    public Transform wheel_L1_Transform;
    public Transform wheel_L2_Transform;
    public Transform wheel_L3_Transform;
    public Transform wheel_L4_Transform;
    public Transform wheel_L5_Transform;
    public Transform wheel_L6_Transform;
    public Transform wheel_L7_Transform;
    public Transform wheel_L8_Transform;
    public Transform wheel_L9_Transform;
    public Transform wheel_L10_Transform;

    public WheelCollider wheel_R1_Collider;
    public WheelCollider wheel_R2_Collider;
    public WheelCollider wheel_R3_Collider;
    public WheelCollider wheel_R4_Collider;
    public WheelCollider wheel_R5_Collider;
    public WheelCollider wheel_R6_Collider;
    public WheelCollider wheel_R7_Collider;
    public WheelCollider wheel_R8_Collider;
    public WheelCollider wheel_R9_Collider;
    public WheelCollider wheel_R10_Collider;

    public Transform wheel_R1_Transform;
    public Transform wheel_R2_Transform;
    public Transform wheel_R3_Transform;
    public Transform wheel_R4_Transform;
    public Transform wheel_R5_Transform;
    public Transform wheel_R6_Transform;
    public Transform wheel_R7_Transform;
    public Transform wheel_R8_Transform;
    public Transform wheel_R9_Transform;
    public Transform wheel_R10_Transform;

    void Update()
    {
        // Drive forward
        if (Input.GetKey(KeyCode.W))
        {
            wheel_L1_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L2_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L3_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L4_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L5_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L6_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L7_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L8_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L9_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L10_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);

            wheel_R1_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R2_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R3_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R4_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R5_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R6_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R7_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R8_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R9_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R10_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
        }
        // Drive backward
        if (Input.GetKey(KeyCode.S))
        {
            wheel_L1_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L2_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L3_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L4_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L5_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L6_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L7_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L8_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L9_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L10_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);

            wheel_R1_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R2_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R3_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R4_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R5_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R6_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R7_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R8_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R9_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R10_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
        }
        // Idle drive right
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            wheel_L1_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L2_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L3_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L4_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L5_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L6_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L7_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L8_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L9_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L10_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);

            wheel_R1_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R2_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R3_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R4_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R5_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R6_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R7_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R8_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R9_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R10_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
        }
        // Idle drive left
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            wheel_L1_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L2_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L3_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L4_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L5_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L6_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L7_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L8_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L9_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L10_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);

            wheel_R1_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R2_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R3_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R4_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R5_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R6_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R7_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R8_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R9_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R10_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            wheel_L1_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L2_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L3_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L4_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L5_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L6_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L7_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L8_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L9_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_L10_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);

            wheel_R1_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R2_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R3_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R4_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R5_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R6_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R7_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R8_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R9_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_R10_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            wheel_L1_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L2_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L3_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L4_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L5_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L6_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L7_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L8_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L9_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);
            wheel_L10_Transform.Rotate(-m_rotationSpeed, 0.0f, 0.0f);

            wheel_R1_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R2_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R3_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R4_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R5_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R6_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R7_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R8_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R9_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
            wheel_R10_Transform.Rotate(m_rotationSpeed, 0.0f, 0.0f);
        }

    }
}
