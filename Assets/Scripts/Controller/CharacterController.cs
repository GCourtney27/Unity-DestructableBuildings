using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{


    [SerializeField] [Range(0.0f, 360.0f)] float m_rotateRate = 90.0f;
    [SerializeField] [Range(0.0f, 360.0f)] float m_maxSpeed = 10.0f;

    //[SerializeField] [Range(1.0f, 50.0f)] float m_speed = 1.0f;
    //[SerializeField] [Range(0.0f, 360.0f)] float m_rotateSpeed = 1.0f;

    float yaw { get; set; } = 0.0f;
    Rigidbody m_rigidBody = null;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 translate = Vector3.zero;
        //
        // translate.z = Input.GetAxis("Vertical") * m_speed;
        // yaw = yaw + Input.GetAxis("Horizontal") * m_rotateSpeed;
        //
        // transform.rotation = Quaternion.AngleAxis(yaw, Vector3.up);
        // m_rigidBody.AddForce(transform.rotation * translate, ForceMode.Acceleration);
        UpdateMovement();
    }

    void UpdateMovement()
    {
        float rotate = 0.0f;
        float foreward = 0.0f;
        float right = 0.0f;

        // Rotation
        if (Input.GetKey(KeyCode.A)) rotate = -m_rotateRate;
        if (Input.GetKey(KeyCode.D)) rotate = m_rotateRate;

        // Forward Backward
        if (Input.GetKey(KeyCode.W)) foreward = m_maxSpeed;
        if (Input.GetKey(KeyCode.S)) foreward = -m_maxSpeed;

        transform.rotation = transform.rotation * Quaternion.AngleAxis(rotate * Time.deltaTime, Vector3.up);
        Vector3 velocity = transform.rotation * new Vector3(right, 0.0f, foreward);
        transform.position = transform.position + velocity * Time.deltaTime;
    }
}
