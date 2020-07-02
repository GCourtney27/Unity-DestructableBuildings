using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float currentSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float sneakSpeed;
    [SerializeField] float jumpHeight;

    [SerializeField] float camSensitivity;
    [SerializeField] float camClamp;

    [Header("Player Stats")]
    // Stamina
    [SerializeField] float currentStamina;
    [SerializeField] float maxStamina;
    [SerializeField] float staminaRegen;

    // Health
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth;
    [SerializeField] float healthRegen;

    [Header("Player UI")]
    [SerializeField] Image HealthBar;
    [SerializeField] Image HealthIcon;
    [SerializeField] Image StaminaBar;
    [SerializeField] Image StaminaIcon;


    public Camera cam;

    float verticalLook = 0.0f;
    float horizontalLook = 0.0f;

    bool isRunning;
    Vector3 moveDirection;
    Rigidbody rb = null;

    void Start()
    {
        // turn off the cursor
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        // Running
        if(Input.GetKey(KeyCode.LeftShift) && currentStamina > 5)
        {
            currentSpeed = runSpeed;
            currentStamina -= Time.deltaTime * 5;
            isRunning = true;
        }
        // Sneaking
        else if(Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = sneakSpeed;
            isRunning = false;
        }
        // Walking
        else
        {
            currentSpeed = walkSpeed;
            isRunning = false;
        }

        UpdateUI();

        // basic movement
        float verticalMovement = Input.GetAxisRaw("Vertical");
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
         moveDirection = (horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;

        //Camera Shit
        verticalLook += -Input.GetAxisRaw("Mouse Y") * camSensitivity;
        horizontalLook += Input.GetAxisRaw("Mouse X") * camSensitivity;

        verticalLook = Mathf.Clamp(verticalLook, -camClamp, camClamp);

        Vector3 horizontalVec = new Vector3(0, horizontalLook, 0);
        Vector3 verticalVec = new Vector3(verticalLook, 0, 0);

        transform.rotation = Quaternion.Euler(horizontalVec);
        cam.transform.rotation = Quaternion.Euler(verticalLook, horizontalLook, 0);

        
        
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }

        // Cursor Shit
        if (Input.GetKeyDown("escape"))
        {
            // turn on the cursor
            Cursor.lockState = CursorLockMode.None;
        }
    }
     void FixedUpdate()
    {
        Move(); 
    }

    void Move()
    {
        Vector3 yVelfix = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = moveDirection * currentSpeed * Time.deltaTime;
        rb.velocity += yVelfix;
    }

    void UpdateUI()
    {
        if (!isRunning)
        {
            if (currentStamina < maxStamina)
            {
                currentStamina += Time.deltaTime * staminaRegen;
            }
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
        }

        HealthBar.fillAmount = currentHealth / maxHealth;
        HealthIcon.color = HealthBar.color * HealthBar.fillAmount;

        StaminaBar.fillAmount = currentStamina / maxStamina;
        StaminaIcon.color = StaminaBar.color * StaminaBar.fillAmount;
    }
}