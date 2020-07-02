using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointAI : MonoBehaviour
{
    [SerializeField] [Range(1.0f, 25.0f)] float m_lookRadius = 10.0f;
    private bool m_chasingPlayer = false;

    Transform target; //Player
    [SerializeField] Transform m_headRotationTarget;
    [SerializeField] Transform m_gunRotationTarget;
    public Transform goal = null; // Goal trying to run to
    EnemyTank tank;

    public Transform headRotationTarget { get { return m_headRotationTarget; } }
    public Transform gunRotationTarget { get { return m_gunRotationTarget; } }

    public float m_rotationSpeed = 3.0f;
    public float m_movementSpeed = 2.0f;
    public bool reachedGoal = false;
    
    void Start()
    {
        tank = GetComponent<EnemyTank>();
        target = PlayerManager.instance.player.transform;
    }
    
    void Update()
    {
        MoveToGoal();
        TurretFacePlayer();
        //FaceTarget("Player");
    }

    void MoveToGoal()
    {
        float movementStep = m_movementSpeed * Time.deltaTime;
        float rotationStep = m_rotationSpeed * Time.deltaTime;

        Vector3 directionToGoal = goal.position - transform.position;
        Quaternion rotationToGoal = Quaternion.LookRotation(directionToGoal);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToGoal, rotationStep);

        //transform.position = Vector3.MoveTowards(transform.position, goal.position, movementStep); // just apply torque to all wheels instead


        float distance = Vector3.Distance(transform.position, goal.position);
        if(distance < 5.0f)
        {
            reachedGoal = true;
        }
        else
        {
            reachedGoal = false;
        }

    }

    void FaceTarget(string chase)
    {
        // Turns turret to face the player
        if (chase == "Player")
        {
            Vector3 direction = (target.position - m_headRotationTarget.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, 0));
            //Quaternion gunRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, 0));

            m_headRotationTarget.transform.rotation = Quaternion.Slerp(m_headRotationTarget.transform.rotation, lookRotation, Time.deltaTime * 5.0f);
            //m_gunRotationTarget.transform.localRotation = Quaternion.Slerp(m_gunRotationTarget.transform.localRotation, gunRotation, Time.deltaTime * 5.0f);
            //m_headRotationTarget.transform.rotation = Quaternion.Slerp(m_headRotationTarget.transform.rotation, lookRotation, Time.deltaTime * 5.0f);
        }
        if (chase == "Goal")
        {
            Vector3 direction = (goal.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5.0f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_lookRadius);
    }

    void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(target.position, transform.position);
        if (distanceToPlayer <= m_lookRadius)
        {
            m_chasingPlayer = true;
            Debug.Log("Shoot Player");
            tank.Shoot = true;
            
        }
        if (distanceToPlayer >= m_lookRadius)
        {
            m_chasingPlayer = false;
        }
    }

    void TurretFacePlayer()
    {
        Vector3 direction = (target.position - m_headRotationTarget.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        m_headRotationTarget.transform.rotation = Quaternion.Slerp(m_headRotationTarget.transform.rotation, lookRotation, Time.deltaTime * 5.0f);

        tank.Shoot = true; // UNCOMMENT TO MAKE TANK SHOOT PLAYER!
    }
}
