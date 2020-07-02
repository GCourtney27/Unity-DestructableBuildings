using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    public Transform cameraTransform;
    Quaternion origionalRotation;

    void Start()
    {
        origionalRotation = transform.rotation;
    }
    
    void Update()
    {
        transform.rotation = cameraTransform.rotation * origionalRotation;
    }
}
