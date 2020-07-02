using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Statistics")]
    [SerializeField] float m_damage = 0.0f;
    [SerializeField] float m_range = 0.0f;
    [SerializeField] float m_fireRate = 0.5f;
    float m_fireTimer = 0.0f;

    [Header("Design")]
    [SerializeField] AudioSource m_fireSound = null;
    [SerializeField] Camera m_fpsCam = null;

    void Start()
    {
        m_fireTimer = m_fireRate;
    }
    
    void Update()
    {

        if(canFire())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                m_fireTimer = m_fireRate;
                Shoot();
            }
        }
    }

    bool canFire()
    {
        m_fireTimer -= Time.deltaTime;
        if(m_fireTimer <= 0.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Shoot()
    {
        //if(!m_fireSound.isPlaying)
        m_fireSound.Play();

        RaycastHit hit;
        if(Physics.Raycast(m_fpsCam.transform.position, m_fpsCam.transform.forward, out hit, m_range))
        {
            Debug.Log(hit.transform.name);
            if(hit.transform.tag == "Enemy")
            {
                //hit.transform.GetComponent<Health>().Damage(m_damage);
                hit.transform.gameObject.GetComponent<Health>().Damage(m_damage);
            }
        }
    }
}
