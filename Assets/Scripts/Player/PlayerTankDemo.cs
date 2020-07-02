using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using System;

public class PlayerTankDemo : MonoBehaviour
{
    [SerializeField] float m_turretRotationTimer = 1.0f;

    [SerializeField] Animator m_animator = null;

    [SerializeField] [Range(0.0f, 3.0f)] float m_turretRotateRate = 0.05f;
    [SerializeField] Transform m_headRotationTarget = null;
    [SerializeField] Transform m_headRotation = null;
    [SerializeField] Transform m_gunRotationTarget = null;

    [Header("Primary Weapon")]
    [SerializeField] Transform m_primaryMuzzle = null;
    [SerializeField] GameObject m_cannonProjectile = null;
    [SerializeField] [Range(0.0f, 10.0f)] float m_cannonFireRate = 10.0f;
    [SerializeField] float m_cannonRecoil = 0.0f;
    float cannonNextFire = 0.0f;
    [SerializeField] GameObject m_emptyShell = null;
    [SerializeField] Transform m_shellEjectPoint = null;

    [Header("Secondary Weapon")]
    [SerializeField] Transform m_secondaryMuzzle = null;
    [SerializeField] [Range(0.0f, 5.0f)] float m_machineGunFireRate = 0.32f;
    [SerializeField] [Range(0.0f, 500.0f)] float m_machineGunAmmo = 0.0f;
    [SerializeField] GameObject m_mgProjectile = null;
    [SerializeField] AudioSource m_machineGunSFX = null;

    // Sounds
    // Weapons
    [Header("Sounds")]
    //[SerializeField] AudioSource m_machineGunSFX = null;
    //[SerializeField] AudioSource m_cannonSFX = null;
    [SerializeField] AudioSource m_idle = null;
    [SerializeField] AudioSource m_beginDrive = null; 
    [SerializeField] AudioSource m_endDrive = null;
    [SerializeField] AudioSource m_driving = null;
    [SerializeField] AudioClip[] m_thirdPersonCannonFiring = null;
    [SerializeField] AudioClip[] m_firstPersonCannonFiring = null;
    [SerializeField] AudioSource m_cannonFiring = null;
    [SerializeField] AudioSource m_turretRotating = null;


    public AudioSource idleEngine { get { return m_idle; } }
    Rigidbody rb = null;
    [SerializeField] Transform CannonBlastPoint = null;
     NavMeshAgent agent;
    float mgFireTimer { get; set; }
    float cannonFireTimer { get; set; }
    bool isMachineGunActive = false;
    bool cannonReloadComplete = true;
    float gunPitch { get; set; }
    Quaternion gunRotation { get; set; }
    bool isMachineGunEmpty = false;

    void Start()
    {
        AssignVariables();
        mgFireTimer = 0.0f;
        gunPitch = 0.0f;
        gunRotation = m_gunRotationTarget.rotation;
        cannonFireTimer = m_cannonFireRate;
    }
    
    void Update()
    {
        FireWeapon();
        ReloadCannon();
        UpdateTurretMovement();
    }

    void AssignVariables()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
    }


    public float m_pitchClamp = 0.0f;
    void UpdateTurretMovement()
    {
        

        float moveTime = 0.0f;
        //if(!isInFirstPersonView)
        //{
        //    m_headRotationTarget.rotation = m_headRotationTarget.rotation * Quaternion.AngleAxis(Input.GetAxis("Mouse X"), Vector3.up);

        //    if (m_headRotation.rotation != m_headRotationTarget.rotation)
        //    {
        //        if (!Input.GetMouseButton(1))
        //        {
        //            moveTime += Time.deltaTime * m_turretRotateRate;
        //            m_headRotation.rotation = Quaternion.Lerp(m_headRotation.rotation, m_headRotationTarget.rotation, m_turretRotateRate * Time.deltaTime);
        //            //m_turretCrosshairs.transform.Translate(new Vector3(m_headRotation.position.x, m_headRotation.position.y, 0.0f));
        //            if(!m_turretRotating.isPlaying)
        //            {
        //                m_turretRotating.Play();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        m_turretRotating.Stop();
        //    }
        //}
        
        //if(isInFirstPersonView)
        //{
        //    m_headRotationTarget.rotation = m_headRotationTarget.rotation * Quaternion.AngleAxis(Input.GetAxis("Mouse X"), Vector3.up);
        //    m_headRotation.rotation = m_headRotationTarget.rotation * Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 0.8f, Vector3.up);

        //}
        
        //m_headRotationTarget.rotation = m_headRotationTarget.rotation * Quaternion.AngleAxis(Input.GetAxis("Mouse X") * m_turretRotateRate, Vector3.up);

    }

    void FireWeapon()
    {
        if (cannonReloadComplete)
        {
            // FireCannon
            Debug.Log("Cannon Firing");
            cannonReloadComplete = false;
            m_animator.SetBool("isFiring", true);
            rb.AddExplosionForce(m_cannonRecoil, CannonBlastPoint.position, 10.0f, 10.0f, ForceMode.Impulse);

            Instantiate(m_cannonProjectile, m_primaryMuzzle.position, m_primaryMuzzle.rotation);

            GameObject go = Instantiate(m_emptyShell, m_shellEjectPoint);
            go.GetComponent<Rigidbody>().AddExplosionForce(4.0f, m_shellEjectPoint.position, 10.0f, 1.0f, ForceMode.Impulse);
        }


        //if (!isMachineGunActive)
        //{
        //    if(Input.GetMouseButtonDown(0) && Time.time > cannonNextFire)
        //    {
        //        m_animator.SetBool("isFiring", true);
        //        rb.AddExplosionForce(m_cannonRecoil, CannonBlastPoint.position, 10.0f, 10.0f, ForceMode.Impulse);
        //        ApplyBlastToNearbyObjects();
        //        if (isInFirstPersonView)
        //        {
        //            // play first person firing
        //            int index = (int)UnityEngine.Random.Range(0.0f, 2.0f);
        //            m_cannonFiring.clip = m_firstPersonCannonFiring[index];
        //        }
        //        if(!isInFirstPersonView)
        //        {
        //            // play third person sounds
        //            int index = (int)UnityEngine.Random.Range(0.0f, 2.0f);
        //            m_cannonFiring.clip = m_thirdPersonCannonFiring[index];
        //        }
        //        m_cannonFiring.Play();

        //        cannonNextFire = Time.time + m_cannonFireRate;
        //        Quaternion rotation = m_primaryMuzzle.rotation * Quaternion.AngleAxis(40.0f, Vector3.up);
        //        Instantiate(m_cannonProjectile, m_primaryMuzzle.position, m_primaryMuzzle.rotation);
        //        cannonReloadComplete = false;
        //    }
            
        //}
        
        //if(isMachineGunActive)
        //{
        //    mgFireTimer = mgFireTimer - Time.deltaTime;
        //    if (mgFireTimer <= 0.0f)
        //    {
        //        if (Input.GetMouseButton(0) && isMachineGunActive && !isMachineGunEmpty)
        //        {
        //            m_machineGunAmmo--;
        //            m_mgAmmoUI.text = m_machineGunAmmo.ToString();
        //            if (m_machineGunAmmo <= 0)
        //            {
        //                isMachineGunEmpty = true;
        //            }
        //            m_machineGunSFX.Play();
        //            mgFireTimer = m_machineGunFireRate;
        //            Quaternion rotation = m_secondaryMuzzle.rotation * Quaternion.AngleAxis(40.0f, Vector3.up);
        //            Instantiate(m_mgProjectile, m_secondaryMuzzle.position, m_secondaryMuzzle.rotation);
        //        }
        //    }
        //}
        
    }
    
    void ReloadCannon()
    {
        cannonFireTimer = cannonFireTimer - Time.deltaTime;
        if (cannonFireTimer <= 0.0f)
        {
            cannonFireTimer = m_cannonFireRate;
            cannonReloadComplete = true;

            m_animator.SetBool("isFiring", false);
        }
    }
    
    void BeginDrive()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {

            //Increase particle effect
            m_beginDrive.Play();
            m_driving.Play();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            m_beginDrive.Stop();
            m_driving.Stop();
            m_endDrive.Play();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_endDrive.Stop();
            m_beginDrive.Play();
            m_driving.Play();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            m_beginDrive.Stop();
            m_driving.Stop();
            m_endDrive.Play();
        }


        //yield return null;
    }
    
}
