using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    [SerializeField] [Range(1.0f, 25.0f)] float m_lookRadius = 10.0f;
    private bool m_chasingPlayer = false;

    Transform target;
    [SerializeField] Transform m_headRotationTarget;
    public Transform goal = null;
    EnemyTank tank;
    NavMeshAgent agent;
    public Transform headRotationTarget;

    void Start()
    {
        headRotationTarget = m_headRotationTarget;
        target = PlayerManager.instance.player.transform;
        //goal = GoalManager.instance.goal.transform;
        tank = GetComponent<EnemyTank>();
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        MoveToGoal();
        //ChasePlayer();
        //TurretFacePlayer();
    }

    void MoveToGoal()
    {
        if (m_chasingPlayer == false)
        {
            tank.Shoot = false;
            float distanceToGoal = Vector3.Distance(goal.transform.position, transform.position);
            //
            agent.SetDestination(goal.position);
            if (distanceToGoal <= agent.stoppingDistance)
            {
                FaceTarget("Goal");
            }
        }
    }

    void TurretFacePlayer()
    {
        Vector3 direction = (target.position - m_headRotationTarget.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        m_headRotationTarget.transform.rotation = Quaternion.Slerp(m_headRotationTarget.transform.rotation, lookRotation, Time.deltaTime * 5.0f);

        tank.Shoot = true; // UNCOMMENT TO MAKE TANK SHOOT PLAYER!
    }

    void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(target.position, transform.position);
        if (distanceToPlayer <= m_lookRadius)
        {
            m_chasingPlayer = true;
            Debug.Log("Shoot Player");
            tank.Shoot = true;
            agent.SetDestination(target.position);
            if (distanceToPlayer <= agent.stoppingDistance)
            {
                FaceTarget("Player");
            }
        }
        if(distanceToPlayer >= m_lookRadius)
        {
            m_chasingPlayer = false;
        }
    }

    void FaceTarget(string chase)
    {
        // Turns turret to face the player
        if(chase == "Player")
        {
            Vector3 direction = (target.position - m_headRotationTarget.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            m_headRotationTarget.transform.rotation = Quaternion.Slerp(m_headRotationTarget.transform.rotation, lookRotation, Time.deltaTime * 5.0f);
        }
        if(chase == "Goal")
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
}
