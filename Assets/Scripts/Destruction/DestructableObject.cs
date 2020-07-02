using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] AudioSource m_destructionBAS = null;
    [SerializeField] AudioClip[] m_destructionSounds = null;


    void Start()
    {

    }
    
    void Update()
    {
        
    }

    public void PlayDestructionSound()
    {
        int index = Random.Range(0, m_destructionSounds.Length);
        m_destructionBAS.clip = m_destructionSounds[index];
        m_destructionBAS.Play();
    }

}
