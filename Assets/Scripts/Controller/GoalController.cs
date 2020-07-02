using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField] [Range(1.0f, 25.0f)] float m_lookRadius = 10;

    EnemyTank enemyTank;
    
    void Start()
    {
        enemyTank = EnemyTankManager.instance.enemyTank;
    }
    
    void Update()
    {
        //float distanceToEnemyTank = Vector3.Distance(enemyTank.transform.position, transform.position);

        //if(distanceToEnemyTank <= lookRadius)
        //{
        //    Debug.Log("Game Over");
        //}
    }

    private void OnTriggerEnter(Collider collidingObject)
    {
        if(collidingObject.CompareTag("Enemy"))
        {
            //Debug.Log("Game Over");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, m_lookRadius);
    }
}
