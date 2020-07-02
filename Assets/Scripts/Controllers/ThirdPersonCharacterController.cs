using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
    [Header("First Person Controls")]
    [SerializeField] [Range(1.0f, 50.0f)] float m_speed = 1.0f;
    [SerializeField] [Range(1.0f, 360.0f)] float m_rotateSpeed = 1.0f;
    [SerializeField] [Range(1.0f, 50.0f)] float m_sensitivity = 1.0f;
    [SerializeField] [Range(1.0f, 89.0f)] float m_pitchClamp = 1.0f;
    //[SerializeField] Camera m_camera = null;
    [SerializeField] bool m_enableMouse = false;

    

    public int m_ammo = 0;
    public float m_fireRate = 0.0f;
    public float m_fireTimer = 0.0f;

    float yaw { get; set; } = 0.0f;
    float pitch { get; set; } = 0.0f;

    Animator m_animator = null;

    void Start()
    {
        m_animator = GetComponent<Animator>();

        //if (m_camera == null)
        //{
        //    m_camera = Camera.main;
        //}
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        //FireWeapon();
        UpdateMovement();
        UpdateAnimations();

    }

   

    void UpdateAnimations()
    {
        if (Input.GetKey(KeyCode.W))
        {
            m_animator.SetBool("isForwardWalking", true);
        }
        if (!Input.GetKey(KeyCode.W))
        {
            m_animator.SetBool("isForwardWalking", false);
        }
        //if (Input.GetKey(KeyCode.S))
        //{
        //    m_animator.SetBool("isBackwardWalking", true);
        //}
        //if (!Input.GetKey(KeyCode.S))
        //{
        //    m_animator.SetBool("isBackwardWalking", false);
        //}
        //if(Input.GetMouseButtonDown(0))
        //{
        //    m_animator.SetBool("isStandingFiring", true);
        //}

        //if (Input.GetKey(KeyCode.X) && m_animator.GetBool("isCrouching"))
        //{
        //    m_animator.SetBool("isCrouching", false);
        //}
        //if (Input.GetKey(KeyCode.X))
        //{
        //    m_animator.SetBool("isCrouching", true);
        //}

    }

    void UpdateMovement()
    {
        Vector3 translate = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) translate.z += m_speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) translate.z -= m_speed * Time.deltaTime;

        if (m_enableMouse)
        {
            yaw = yaw + Input.GetAxis("Mouse X") * m_sensitivity;
            if (Input.GetKey(KeyCode.A)) translate.x -= m_speed * Time.deltaTime;
            if (Input.GetKey(KeyCode.D)) translate.x += m_speed * Time.deltaTime;
        }
        else
        {
            if (Input.GetKey(KeyCode.A)) yaw -= m_rotateSpeed * Time.deltaTime;
            if (Input.GetKey(KeyCode.D)) yaw += m_rotateSpeed * Time.deltaTime;
        }

        pitch = pitch + -Input.GetAxis("Mouse Y") * m_sensitivity;
        pitch = Mathf.Clamp(pitch, -m_pitchClamp, m_pitchClamp);

        transform.rotation = Quaternion.AngleAxis(yaw, Vector3.up);

        transform.Translate(translate, Space.Self);

        //m_camera.fieldOfView += Input.GetAxis("Mouse ScrollWheel") * 15.0f;
        //m_camera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        //
        //transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
