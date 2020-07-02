using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 5.0f)] float m_destroyDelay = 0.0f;
    [SerializeField] GameObject m_explosion = null;
    [SerializeField] bool m_activateRigidBody = false;
    [SerializeField] [Range(0, 1000)] int m_points = 0;

    bool m_destroyed = false;
    void DestroyGameObject()
    {
        if (m_destroyed == false)
        {
            m_destroyed = true;
            //Game.Instance.AddPoints(m_points);
            //if (m_activateRigidBody)
            //{
            //    GetComponent<Rigidbody>().isKinematic = false;
            //}
            if (m_explosion)
            {
                if(this.tag == "Enemy")
                {
                    //m_explosion.GetComponent<EnemyTankExplosion>().headRotationTarget.rotation = this.GetComponent<EnemyController>().headRotationTarget.rotation;
                    m_explosion.GetComponent<EnemyTankExplosion>().headRotationTarget.rotation = this.GetComponentInParent<WaypointAI>().headRotationTarget.rotation;
                }
                Instantiate(m_explosion, transform.parent.position, transform.rotation);
            }
            if (this.tag == "Enemy")
            {
                Destroy(transform.parent.gameObject, m_destroyDelay);
            }
        }


    }
}
