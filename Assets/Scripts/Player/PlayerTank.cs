using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using System;

public class PlayerTank : MonoBehaviour
{

    [SerializeField] Animator m_animator = null;

    [SerializeField] Camera m_thirdPersonCamera = null;
    [SerializeField] Camera m_firstPersonCamera = null;

    [SerializeField] [Range(0.0f, 100.0f)] float m_turretRotateRate = 0.05f;
    [SerializeField] Transform m_headRotationTarget = null;
    [SerializeField] Transform m_headRotation = null;
    [SerializeField] Transform m_gunRotationTarget = null;
    [SerializeField] LayerMask m_blastEffect;

    [Header("Primary Weapon")]
    [SerializeField] Transform m_primaryMuzzle = null;
    [SerializeField] GameObject m_cannonProjectile = null;
    public GameObject cannonProjectile { get { return m_cannonProjectile; } }
    [SerializeField] [Range(0.0f, 10.0f)] float m_cannonFireRate = 10.0f;
    [SerializeField] float m_cannonRecoil = 0.0f;
    float cannonNextFire = 0.0f;

    [Header("Secondary Weapon")]
    [SerializeField] Transform m_secondaryMuzzle = null;
    [SerializeField] [Range(0.0f, 5.0f)] float m_machineGunFireRate = 0.32f;
    [SerializeField] [Range(0.0f, 500.0f)] float m_machineGunAmmo = 0.0f;
    [SerializeField] GameObject m_mgProjectile = null;
    [SerializeField] AudioSource m_machineGunSFX = null;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI m_cannonStatus = null;
    [SerializeField] TextMeshProUGUI m_mgAmmoUI = null;
    [SerializeField] TextMeshProUGUI m_mgStatusText = null;
    [SerializeField] TextMeshProUGUI m_cannonStatusText = null;
    [SerializeField] Transform m_turretCrosshairs = null;

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

    public bool isInFirstPersonView = false;

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
    public bool isFiring = false;

    float m_maxAmmo = 30.0f;
    bool limitAmmo = false;
    void Start()
    {
        AssignVariables();
        mgFireTimer = 0.0f;
        gunPitch = 0.0f;
        gunRotation = m_gunRotationTarget.rotation;
        cannonFireTimer = m_cannonFireRate;
        agent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        //m_idle.Play();
        m_mgAmmoUI.text = m_machineGunAmmo.ToString();
        //StartCoroutine("BeginDrive");
    }
    
    void Update()
    {
        //BeginDrive();
        ToggleFirstPerson();
        ChangeCameraState();
        UpdateHealth();
        CheckWeaponStates();
        FireWeapon();
        ReloadCannon();
        CheckZoomedView();
        UpdateTurretMovement();
        UIReloadStatus();
        highlightCriticalAmmo();
        highlightActiveWeapon();
    }

    void AssignVariables()
    {
        rb = GetComponent<Rigidbody>();
    }

    

    void highlightActiveWeapon()
    {
        if (isMachineGunActive)
        {
            m_mgStatusText.color = Color.white;
            m_cannonStatusText.color = Color.gray;
        }
        else
        {
            m_mgStatusText.color = Color.gray;
            m_cannonStatusText.color = Color.white;
        }
    }

    void highlightCriticalAmmo()
    {
        if(m_machineGunAmmo < 150.0f)
        {
            m_mgAmmoUI.color = Color.red;
        }
    }

    void UIReloadStatus()
    {
        if (cannonReloadComplete == true)
        {
            if (limitAmmo)
            {
                m_cannonStatus.text = "Ready " + m_maxAmmo;
                return;
            }
            m_cannonStatus.text = "Ready";
        }
        if (cannonReloadComplete == false)
        {
            m_cannonStatus.text = "Reloading ";
        }
    }

    void CheckZoomedView()
    {
        //if(Input.GetMouseButtonDown(1))
        //{
        //    Camera.main.fieldOfView = 20;
        //}
        //if (Input.GetMouseButtonUp(1))
        //{
        //    Camera.main.fieldOfView = 60;
        //}
    }
    
    void UpdateTurretMovement()
    {
        float moveTime = 0.0f;
        if(!isInFirstPersonView)
        {
            m_headRotationTarget.rotation = m_headRotationTarget.rotation * Quaternion.AngleAxis(Input.GetAxis("Mouse X"), Vector3.up);

            if (m_headRotation.rotation != m_headRotationTarget.rotation)
            {
                if (!Input.GetMouseButton(1))
                {
                    moveTime += Time.deltaTime * m_turretRotateRate;
                    m_headRotation.rotation = Quaternion.RotateTowards(m_headRotation.rotation, m_headRotationTarget.rotation, m_turretRotateRate * Time.deltaTime);// Rotatetowards
                    //m_turretCrosshairs.transform.Translate(new Vector3(m_headRotation.position.x, m_headRotation.position.y, 0.0f));
                    float angle = Vector3.Dot(m_headRotationTarget.position, m_headRotation.position);
                    //Debug.Log(angle);
                    if(!m_turretRotating.isPlaying)
                    {
                        //m_turretRotating.Play(); // Play turret rotation sound
                    }
                }
            }
            else
            {
                m_turretRotating.Stop();
            }
        }
        
        if(isInFirstPersonView)
        {
            m_headRotationTarget.rotation = m_headRotationTarget.rotation * Quaternion.AngleAxis(Input.GetAxis("Mouse X"), Vector3.up);
            m_headRotation.rotation = m_headRotationTarget.rotation * Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 0.8f, Vector3.up);

        }
        
        //m_headRotationTarget.rotation = m_headRotationTarget.rotation * Quaternion.AngleAxis(Input.GetAxis("Mouse X") * m_turretRotateRate, Vector3.up);

    }

    void UpdateHealth()
    {
        //float health = GetComponent<Health>().health / 100.0f;
        //m_healthUI.text = "Health: " + health.ToString("P1");
    }

    void ToggleFirstPerson()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isInFirstPersonView = !isInFirstPersonView;
        }
    }

    void ChangeCameraState()
    {
        if(isInFirstPersonView)
        {
            // position camera to first person
            m_thirdPersonCamera.enabled = false;
            m_thirdPersonCamera.GetComponent<AudioListener>().enabled = false;

            m_firstPersonCamera.enabled = true;
            m_firstPersonCamera.GetComponent<AudioListener>().enabled = true;
        }
        if (!isInFirstPersonView)
        {
            // position camera to third person
            m_thirdPersonCamera.enabled = true;
            m_thirdPersonCamera.GetComponent<AudioListener>().enabled = true;

            m_firstPersonCamera.enabled = false;
            m_firstPersonCamera.GetComponent<AudioListener>().enabled = false;
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
                rb.AddExplosionForce(400.0f, CannonBlastPoint.position, 70.0f, 1.0f);
            }
        }
    }

    public void activateAmmoCap()
    {
        limitAmmo = true;
    }

    void FireWeapon()
    {
        if (!isMachineGunActive)
        {
            if(Input.GetMouseButtonDown(0) && Time.time > cannonNextFire)
            {
                isFiring = true;
                if (m_maxAmmo <= 10) m_cannonStatus.color = Color.red;
                if (m_maxAmmo <= 0) return;
                if (limitAmmo) m_maxAmmo--;

                m_animator.SetBool("isFiring", true);
                rb.AddExplosionForce(m_cannonRecoil, CannonBlastPoint.position, 10.0f, 10.0f, ForceMode.Impulse);
                ApplyBlastToNearbyObjects();
                if (isInFirstPersonView)
                {
                    // play first person firing
                    int index = (int)UnityEngine.Random.Range(0.0f, 2.0f);
                    m_cannonFiring.clip = m_firstPersonCannonFiring[index];
                }
                if(!isInFirstPersonView)
                {
                    // play third person sounds
                    int index = (int)UnityEngine.Random.Range(0.0f, 2.0f);
                    m_cannonFiring.clip = m_thirdPersonCannonFiring[index];
                }
                m_cannonFiring.Play();

                cannonNextFire = Time.time + m_cannonFireRate;
                Quaternion rotation = m_primaryMuzzle.rotation * Quaternion.AngleAxis(40.0f, Vector3.up);
                Instantiate(m_cannonProjectile, m_primaryMuzzle.position, m_primaryMuzzle.rotation);
                cannonReloadComplete = false;

            }

        }
        
        if(isMachineGunActive)
        {
            mgFireTimer = mgFireTimer - Time.deltaTime;
            if (mgFireTimer <= 0.0f)
            {
                if (Input.GetMouseButton(0) && isMachineGunActive && !isMachineGunEmpty)
                {
                    m_machineGunAmmo--;
                    m_mgAmmoUI.text = m_machineGunAmmo.ToString();
                    if (m_machineGunAmmo <= 0)
                    {
                        isMachineGunEmpty = true;
                    }
                    m_machineGunSFX.Play();
                    mgFireTimer = m_machineGunFireRate;
                    Quaternion rotation = m_secondaryMuzzle.rotation * Quaternion.AngleAxis(40.0f, Vector3.up);
                    Instantiate(m_mgProjectile, m_secondaryMuzzle.position, m_secondaryMuzzle.rotation);
                }
            }
        }
        
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

    void CheckWeaponStates()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            isMachineGunActive = !isMachineGunActive;
        }
    }
    
    
    
}
