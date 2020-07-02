using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTank : MonoBehaviour
{
    
    //[SerializeField] Transform m_head = null;
    [SerializeField] Transform m_Muzzle = null;
    [SerializeField] GameObject m_projectile = null;
    [SerializeField] AudioSource m_fireSFX = null;
    public GameObject cannonProjectile { get { return m_projectile; } }
    public LayerMask m_blastEffect;

    //[SerializeField] [Range(0.0f, 5.0f)] float m_fireRate = 3.0f;
    float m_fireRate = 0.0f;
    GameObject m_target = null;

    public bool Shoot = false;
    public bool isTurretDisabled = false;

    NavMeshAgent agent;
    Rigidbody rb = null;
    [SerializeField] float m_cannonRecoil = 10.0f;
    [SerializeField] Transform CannonBlastPoint = null;
    float fireTimer { get; set; }

    private void Awake()
    {
        m_fireRate = Random.Range(3.0f, 7.0f);
    }

    void Start()
    {
        fireTimer = m_fireRate;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if(Shoot)
        {
            if(!isTurretDisabled)
            {
                //Debug.Log("Tank is Shooting");
                fireTimer = fireTimer - Time.deltaTime;
                if (fireTimer <= 0.0f)
                {
                    Instantiate(m_projectile, m_Muzzle.position, m_Muzzle.rotation);
                    rb.AddExplosionForce(m_cannonRecoil, CannonBlastPoint.position, 10.0f, 10.0f, ForceMode.Impulse);
                    ApplyBlastToNearbyObjects();
                    fireTimer = m_fireRate;
                    if (m_fireSFX) m_fireSFX.Play();
                }
            }
            
        }
    }

    void ApplyBlastToNearbyObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(CannonBlastPoint.position, 15.0f, m_blastEffect, QueryTriggerInteraction.Ignore);
        //Debug.Log(colliders.Length);
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponentInChildren<Rigidbody>();
            //Debug.Log(collider.tag);
            if (rb != null)
            {
                rb.AddExplosionForce(300.0f, CannonBlastPoint.position, 70.0f, 1.0f);
            }
        }
    }

    //private void OnTriggerEnter(Collider collidingObject)
    //{
    //    //if (collidingObject.CompareTag("Player"))
    //    //{
    //    //    m_target = collidingObject.gameObject;
    //    //}
    //}

    //private void OnTriggerExit(Collider collidingObject)
    //{
    //    //if (collidingObject.CompareTag("Player"))
    //    //{
    //    //    m_target = null;
    //    //}
    //}
}
