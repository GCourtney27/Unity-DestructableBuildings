using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScope : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 100.0f)] float m_scopeHealth = 1.0f;
    [SerializeField] [Range(0.0f, 10.0f)] float m_damageToTank = 1.0f;

    [SerializeField] AudioSource m_turretDisable = null;
    [SerializeField] EnemyTank m_owner = null;
    [SerializeField] ParticleSystem m_particleSystem = null;
    [SerializeField] [Range(0.0f, 10.0f)] float m_mgDamage = 1.0f;

    bool disabled = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MachineGunProjectile"))
        {
            m_scopeHealth -= m_mgDamage;
            if (m_scopeHealth < 0 && !disabled)
            {
                disabled = true;
                m_turretDisable.Play();
                m_particleSystem.Play();
                m_owner.isTurretDisabled = true;
                m_owner.GetComponent<Health>().health -= m_damageToTank;
            }
            // Check health of scope
            // if scope health is 0 disable enemy cannon
        }
    }


}
