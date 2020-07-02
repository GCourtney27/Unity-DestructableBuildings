using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAround : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 50.0f)] float m_xSpread = 0.0f;
    [SerializeField] [Range(0.0f, 50.0f)] float m_zSpread = 0.0f;
    [SerializeField] [Range(0.0f, 50.0f)] float yOffset = 0.0f;
    [SerializeField] public Transform m_centerPoint;

    [SerializeField] [Range(0.0f, 1.0f)] float m_rotationSpeed = 0.5f;
    [SerializeField] bool m_rotateClockwise;
    

    float timer = 0;
    void Update()
    {
        timer += Time.deltaTime * m_rotationSpeed;
        Rotate();
    }

    void Rotate()
    {
        if(m_rotateClockwise)
        {
            float x = -Mathf.Cos(timer) * m_xSpread;
            float z = Mathf.Sin(timer) * m_zSpread;
            Vector3 pos = new Vector3(x, yOffset, z);
            transform.position = pos + m_centerPoint.position;
        }
        else
        {
            float x = Mathf.Cos(timer) * m_xSpread;
            float z = Mathf.Sin(timer) * m_zSpread;
            Vector3 pos = new Vector3(x, yOffset, z);
            transform.position = pos + m_centerPoint.position;
        }
    }

}
