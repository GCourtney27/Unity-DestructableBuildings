using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] Transform m_projectileInstancePoint = null;
    [SerializeField] GameObject m_projectile = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Instantiate(m_projectile, m_projectileInstancePoint);

        }
    }
}
