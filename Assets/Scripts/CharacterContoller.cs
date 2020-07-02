using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContoller : MonoBehaviour
{
    public float m_speed = 10.0f;

    public Animator m_animator = null;

    [Header("Weapon")]
    [SerializeField] [Range(0.0f, 500.0f)] float m_force = 10.0f;
    [SerializeField] [Range(0.0f, 10.0f)] float m_projectileLifetime = 1.5f;
    [SerializeField] GameObject m_explosion = null;
    [SerializeField] GameObject m_projectile = null;
    [SerializeField] Transform m_projectileInstancePoint = null;

    public int m_ammo = 0;
    public float m_fireRate = 1.0f;
    float m_fireTimer = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            m_speed = 5.0f;
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            m_speed = 2.5f;
        }

        float translation = Input.GetAxis("Vertical") * m_speed;
        float straffe = Input.GetAxis("Horizontal") * m_speed;

        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(new Vector3(straffe, 0.0f, translation));

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        UpdataAnimations();
        FireWeapon();

    }

    private void FireWeapon()
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

    public void Fire()
    {
        Instantiate(m_projectile, m_projectileInstancePoint.position, m_projectileInstancePoint.rotation);

        // Destroy object after lifetime has expired
        //Destroy(go, m_projectileLifetime);
    }

    void UpdataAnimations()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            m_animator.SetBool("isRunning", true);
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift))
        {
            m_animator.SetBool("isRunning", false);
        }
        if (Input.GetKey(KeyCode.W))
        {
            m_animator.SetBool("isForwardWalking", true);
        }
        if (!Input.GetKey(KeyCode.W))
        {
            m_animator.SetBool("isForwardWalking", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_animator.SetBool("isBackwardWalking", true);
        }
        if (!Input.GetKey(KeyCode.S))
        {
            m_animator.SetBool("isBackwardWalking", false);
        }

        
    }
}
