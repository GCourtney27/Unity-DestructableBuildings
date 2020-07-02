using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankExplosion : MonoBehaviour
{
    [SerializeField] ParticleSystem[] m_sparks = null;
    [SerializeField] ParticleSystem m_mainExplosion = null;
    [SerializeField] Transform m_headRotationTarget = null;
    [SerializeField] AudioSource m_TankDestruction = null;

    public float m_mainExplosionDelay = 0.0f;
    bool isDestroyed = false;
    public Transform headRotationTarget;

    void Start()
    {
        m_TankDestruction.Play();

        headRotationTarget = m_headRotationTarget;

        foreach(ParticleSystem ps in m_sparks)
        {
            ps.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_mainExplosionDelay -= Time.deltaTime;
        if(m_mainExplosionDelay <= 0)
        {
            foreach (ParticleSystem ps in m_sparks)
            {
                ps.Stop();
            }
            if(!isDestroyed)
            {
                m_mainExplosion.Play();
                isDestroyed = true;
            }
            
        }

        Destroy(gameObject, 5.0f);
    }
}
