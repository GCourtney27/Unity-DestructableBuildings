using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BountyController : MonoBehaviour
{
    NavMeshAgent m_navMeshAgent = null;
    [SerializeField] Transform m_goal = null;
    Animator m_animator = null;

    public Animator animator { get { return m_animator; } }
    public NavMeshAgent nvAgent { get { return m_navMeshAgent; } }

    Health m_health = null;
    bool m_isIdle = true;
    public bool isAlive = true;

    void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_health = GetComponent<Health>();
        m_animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if(!isAlive)
        {
            m_navMeshAgent.isStopped = true;
        }
        if (!m_navMeshAgent.pathPending)
        {
            if (m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
            {
                if (!m_navMeshAgent.hasPath || m_navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    if(!m_isIdle && isAlive)
                    {
                        FPSGameManager.Instance.FPSGame.StartFailedScreen(true);
                    }
                    
                }
            }
        }
        if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("GetUp"))
        {
            m_animator.SetBool("isGettingUp", false);
            m_animator.SetBool("isRunning", true);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerProjectile"))
        {
            // Move to goal
            m_isIdle = false;
            m_navMeshAgent.SetDestination(m_goal.position);
            m_animator.SetBool("isGettingUp", true);
        }
    }
}
