using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon01 : MonoBehaviour
{
    public int m_ammo = 0;
    public float m_fireRate = 0.0f;
    public float m_fireTimer = 0.0f;

   public abstract void Fire();

    public bool canFire()
    {
        m_fireTimer -= Time.deltaTime;
        if (m_fireTimer <= 0.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
