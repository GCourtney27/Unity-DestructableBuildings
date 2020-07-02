using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float speed = 10.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //transform.Rotate(-Input.GetAxis("Mouse Y") * (speed), 0.0f, 0.0f);
        transform.Rotate(0.0f, Input.GetAxis("Mouse X") * (speed), 0.0f);
    }
}
