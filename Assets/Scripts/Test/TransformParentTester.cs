using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformParentTester : MonoBehaviour
{

    [SerializeField] Transform m_parentTransform = null;
    [SerializeField] Transform m_objectToTransform = null;

    void Start()
    {
        
    }
    
    void Update()
    {
        m_objectToTransform = m_parentTransform;
    }
}
