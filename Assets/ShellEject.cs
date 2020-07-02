using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellEject : MonoBehaviour
{
    Rigidbody m_rb = null;
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        //m_rb.AddForceAtPosition(Vector3.forward, transform.position);
    }
    
    void Update()
    {
        
    }
}
