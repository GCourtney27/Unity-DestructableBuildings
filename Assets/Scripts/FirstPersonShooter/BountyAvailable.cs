using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BountyAvailable : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 100.0f)] float m_availabilityDelay = 1.0f;
    float m_availabilityDelayTimer = 0.0f;
    bool m_isBountyAvailable = false;

    public bool isBountyAvailable { get { return m_isBountyAvailable; } set { m_isBountyAvailable = value; } }

    void Start()
    {
        m_availabilityDelayTimer = m_availabilityDelay;
    }


    void Update()
    {
        if(isReady())
        {
            m_availabilityDelayTimer = m_availabilityDelay;
            m_isBountyAvailable = true;
        }
    }

    bool isReady()
    {
        m_availabilityDelayTimer -= Time.deltaTime;
        if (m_availabilityDelayTimer <= 0.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
