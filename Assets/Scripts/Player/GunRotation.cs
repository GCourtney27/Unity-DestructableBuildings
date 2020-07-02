using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    float m_pitch = 0.0f;
    
    private void Start()
    {
        //2.25
        //origionalRotation = transform.localRotation;
        //transform.localRotation = cameraTransform.rotation * origionalRotation;
        
    }

    void Update()
    {
        //RotatePlayer();
        if (this.tag == "Player") RotatePlayer();
        if (this.tag == "Enemy") RotateEnemy();
    }

    [SerializeField] float startOffset = 1.0f;
    void RotatePlayer()
    {
        SetStartPos();
        m_pitch = m_pitch - Input.GetAxis("Mouse Y");
        m_pitch = Mathf.Clamp(m_pitch, -18.0f, 18.0f);

        Vector3 rotate = new Vector3(m_pitch + startOffset, 0.0f, 0.0f);
        Quaternion qrotate = Quaternion.Euler(rotate);
        transform.localRotation = qrotate;
        if(!Input.GetMouseButton(1))
        {
            //transform.localRotation = Quaternion.RotateTowards(transform.rotation, qrotate, Time.deltaTime);
        }
    }

    void RotateEnemy()
    {
        SetStartPos();
        m_pitch = m_pitch - GetComponent<WaypointAI>().headRotationTarget.transform.rotation.y;
        m_pitch = Mathf.Clamp(m_pitch, -18.0f, 18.0f);

        Vector3 rotate = new Vector3(m_pitch, 0.0f, 0.0f);
        Quaternion qrotate = Quaternion.Euler(rotate);
        transform.localRotation = qrotate;
    }

    bool hasBeenSet = false;
    void SetStartPos()
    {
        if(!hasBeenSet)
        {
            m_pitch = 18.0f;
            Vector3 rotate = new Vector3(m_pitch, 0.0f, 0.0f);
            Quaternion qrotate = Quaternion.Euler(rotate);
            transform.localRotation = qrotate;
            hasBeenSet = true;
        }
    }
}
