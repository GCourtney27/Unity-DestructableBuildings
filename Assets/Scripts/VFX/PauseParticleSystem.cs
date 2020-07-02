using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseParticleSystem : MonoBehaviour
{
    public ParticleSystem particles;
    public float m_timeToPause = 0.0f;

    // Update is called once per frame
    void Update()
    {
        //After 4 seconds, pause particles
        if (Time.timeSinceLevelLoad > m_timeToPause)
        {
            particles.Pause();
        }
    }
}
