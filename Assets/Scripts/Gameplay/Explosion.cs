using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 100.0f)] float m_damage = 100.0f;
    [SerializeField] [Range(0.0f, 100.0f)] float m_radius = 5.0f;
    [SerializeField] [Range(0.0f, 100.0f)] float m_force = 10.0f;

    [SerializeField] LayerMask m_layerMask;
    [SerializeField] AudioClip[] m_tankHit = null;
    [SerializeField] AudioSource m_emptyTankHit = null;
    [SerializeField] AudioSource m_groundExplosion = null;
    [SerializeField] AudioSource m_groundExplosion2 = null;
    
    void Start()
    {
        //Uses m_radius as its collision sphere
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_radius, m_layerMask, QueryTriggerInteraction.Ignore);
        foreach (Collider collider in colliders)
        {


            //if (collider.CompareTag("Terrain")) continue;
            if (collider.CompareTag("Player")) continue;

            Rigidbody coll_rb = collider.GetComponent<Rigidbody>();
            Health coll_health = collider.GetComponent<Health>();

            if (coll_health)
            {
                coll_health.Damage(m_damage);
            }
            if(!coll_health.isAlive() && coll_rb != null)
            {
                if(coll_rb)
                {
                    coll_rb.isKinematic = false;
                    coll_rb.AddExplosionForce(m_force, transform.position, m_radius, 3.0f);
                }
                
                //if (collider.gameObject.CompareTag("Terrain")) return;
            }
        }
        Destroy(gameObject, 3.0f);
    }

    private void OnTriggerEnter(Collider collider)
    {
        
        //if (collider.CompareTag("Terrain"))
        //{
        //    int sound = (int)Random.Range(1.0f, 3.0f);
        //    if (sound == 1)
        //    {
        //        m_groundExplosion.Play();
        //    }
        //    if (sound == 2)
        //    {
        //        m_groundExplosion2.Play();
        //    }
        //}
        //if(collider.CompareTag("Player") || collider.CompareTag("Enemy"))
        //{
        //    int index = (int)Random.Range(0, m_tankHit.Length - 1);
        //    m_emptyTankHit.clip = m_tankHit[index];
        //    m_emptyTankHit.Play();
        //}
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }
}
