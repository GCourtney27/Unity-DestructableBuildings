using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    [SerializeField] float m_distance = 1.0f;
    public Vector3 normal { get; set; } = Vector3.up;

    [SerializeField] [Range(0.0f, 100.0f)] float rotationSpeed = 50.0f;

    Quaternion m_prevSlopeRotation = Quaternion.identity;

    private void Update()
    {
        float time = Time.deltaTime * rotationSpeed;

        //Debug.DrawRay(transform.position, Vector3.down * m_distance, Color.white);
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, m_distance))
        {
            normal = hit.normal;
            Debug.DrawRay(hit.point, hit.normal * 2.0f, Color.red);

            Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, normal);
            slopeRotation = Quaternion.Lerp(m_prevSlopeRotation, slopeRotation, time);
            
            Quaternion rotation = this.GetComponentInParent<CharacterController>().transform.rotation;
            Vector3 angles = rotation.eulerAngles;

            Quaternion yrotation = Quaternion.Euler(0.0f, angles.y, 0.0f);
            Quaternion targetRotation = slopeRotation * yrotation;
            //targetRotation = Quaternion.SlerpUnclamped(rotation, targetRotation, 3.0f * Time.deltaTime);

            this.GetComponentInParent<CharacterController>().transform.rotation = targetRotation;

        }
        //this.GetComponentInParent<CharacterController>().transform.rotation = Quaternion.SlerpUnclamped(this.GetComponentInParent<CharacterController>().transform.rotation, slopeQuatOffset, time);

        //Quaternion.LerpUnclamped(this.GetComponentInParent<CharacterController>().transform.rotation, slopeQuatOffset, Time.deltaTime);

        //this.GetComponentInParent<CharacterController>().transform.rotation = Quaternion.SlerpUnclamped(this.GetComponentInParent<CharacterController>().transform.rotation, slopeQuatOffset, 1.0f);
        //moveDirection = slopeQuatOffset * moveDirection;
    }




}

