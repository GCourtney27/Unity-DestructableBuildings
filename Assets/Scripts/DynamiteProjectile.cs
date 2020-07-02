using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteProjectile : Weapon01
{
    [SerializeField] [Range(0.0f, 500.0f)] float m_force = 10.0f;
    [SerializeField] [Range(0.0f, 10.0f)] float m_lifetime = 1.5f;
    [SerializeField] GameObject m_explosion = null;
    [SerializeField] GameObject m_projectile = null;
    [SerializeField] Transform m_projectileInstancePoint = null;

    Rigidbody m_rb = null;

    void Update()
    {
        if (canFire())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                m_fireTimer = m_fireRate;
                Fire();
            }
        }
    }

    public override void Fire()
    {
        Instantiate(m_projectile, m_projectileInstancePoint);

        // Gives the projectile force to send it forward
        m_rb = GetComponent<Rigidbody>();
        m_rb.AddForce(transform.forward * m_force, ForceMode.Impulse);
        // Destroy object after lifetime has expired
        Destroy(gameObject, m_lifetime);
    }
}
