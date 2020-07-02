using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerSounds : MonoBehaviour
{
    [Header("Engine")]
    [SerializeField] AudioSource m_tigerAwake = null;
    [SerializeField] AudioClip[] m_tigerAwakeClips = null;

    [SerializeField] AudioSource m_idleEngine = null;
    public AudioSource idleEngine { get { return m_idleEngine; } }

    [SerializeField] AudioSource m_beginDrive = null;
    [SerializeField] AudioClip[] m_beginDriveClips = null;

    [SerializeField] AudioSource m_driving = null;

    [SerializeField] AudioSource m_endDrive = null;
    [SerializeField] AudioClip[] m_endDriveClips = null;

    [Header("Tracks")]
    [SerializeField] AudioSource m_tracksBeginDrive = null;
    [SerializeField] AudioClip[] m_tracksBeginDriveClips = null;

    [SerializeField] AudioSource m_tracksDriving = null;
    [SerializeField] AudioClip[] m_tracksDrivingClips = null;

    [SerializeField] AudioSource m_tracksEndDrive = null;
    [SerializeField] AudioClip[] m_tracksEndDriveClips = null;

    Rigidbody m_rb = null;

    void Start()
    {
        m_rb = GetComponentInParent<Rigidbody>();
        
        int awakeIndex = (int)Random.Range(0.0f, m_tigerAwakeClips.Length - 1);
        m_tigerAwake.clip = m_tigerAwakeClips[awakeIndex];
        m_tigerAwake.Play();
    }

    float waitForIdlePlay = 0.7f;
    bool continuePlayingIdle = false;
    bool isBeginDrivePlaying = false;
    bool isEndDrivePlaying = false;

    private Vector3 lastPosition;
    private Vector3 lastVelocity;
    private Vector3 lastAcceleration;

    int test = 0;
    bool continuePlayingBeginDrive = false;

    private void Awake()
    {
        Vector3 position = transform.position;
        Vector3 velocity = Vector3.zero;
        Vector3 acceleration = Vector3.zero;
    }

    void Update()
    {

        float currentVelocity = m_rb.velocity.magnitude;

        if (m_tigerAwake.isPlaying) // Keep tank in place while engine start sound plays
            m_rb.velocity = new Vector3(0, 0, 0);

        waitForIdlePlay -= Time.deltaTime; // Wait for engine start sound so complete before beginning idle sound
        if (waitForIdlePlay <= 0.0f && !continuePlayingIdle)
        {
            m_idleEngine.Play();
            continuePlayingIdle = true;
        }

        //BeginDrive();

        if (!isDriving())
        {
            test = 0;

            StopDriving();

            continuePlayingBeginDrive = false;
            //if (!m_idleEngine.isPlaying)
            //PlayIdle();

        }

        if (isDriving())
        {
            //if(m_idleEngine.isPlaying)
            //StopIdle();

            if (!continuePlayingBeginDrive)
                PlayBeginDrive();

            if (test == 0 && !m_driving.isPlaying && !m_tracksDriving.isPlaying)
                PlayDriving();
        }

        if (isDriving() && isDecelerating())
        {
            StopDriving();
            PlayEndDrive();
        }
        
        //Debug.Log("Tank is driving: " + isDriving());
    }
    
    bool isDecelerating()
    {
        bool result = false;

        if (Input.GetKeyUp(KeyCode.W))
            result = true;

        return result;
    }

    float drivingSpeed;
    bool isDriving()
    {
        drivingSpeed = m_rb.velocity.magnitude;
        if (drivingSpeed < 1.0f)
        {
            return false;
        }
        return true;
    }

    void PlayIdle()
    {
        m_idleEngine.Play();
    }

    void StopIdle()
    {
        m_idleEngine.Stop();
    }

    float playDrivingDelay = 1.0f;
    void PlayBeginDrive()
    {
        //test++;
        continuePlayingBeginDrive = true;
        int indexEngine = (int)Random.Range(0.0f, m_beginDriveClips.Length - 1);
        int indexTracks = (int)Random.Range(0.0f, m_tracksBeginDriveClips.Length - 1);
        m_beginDrive.clip = m_beginDriveClips[indexEngine];
        m_tracksBeginDrive.clip = m_tracksBeginDriveClips[indexTracks];
        
        m_beginDrive.Play();
        m_tracksBeginDrive.Play();


    }

    void StopDriving()
    {
        m_driving.Stop();
        m_tracksDriving.Stop();
    }

    void PlayDriving()
    {
        test++;
        int indexTracks = (int)Random.Range(0, m_tracksDrivingClips.Length - 1);
        m_tracksDriving.clip = m_tracksDrivingClips[indexTracks];

        m_driving.Play();
        m_tracksDriving.Play();
    }

    void StopBeginDrive()
    {
        m_beginDrive.Stop();
        m_tracksBeginDrive.Stop();
    }

    void PlayEndDrive()
    {
        if(!isEndDrivePlaying)
        {
            isEndDrivePlaying = true;

            isBeginDrivePlaying = false;
            int indexEngine = (int)Random.Range(0.0f, m_endDriveClips.Length - 1);
            int indexTracks = (int)Random.Range(0.0f, m_tracksEndDriveClips.Length - 1);
            m_endDrive.clip = m_endDriveClips[indexEngine];
            m_tracksEndDrive.clip = m_tracksEndDriveClips[indexTracks];

            m_driving.Stop();
            m_endDrive.Play();
            m_tracksBeginDrive.Play();
            m_idleEngine.Play();
        }
        
    }

    void StopEndDrive()
    {
        m_endDrive.Stop();
        m_tracksBeginDrive.Stop();
    }

    void BeginDrive()
    {
        if(Input.GetKey(KeyCode.W))
        {
            PlayDriving();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            //Increase particle effect
            if(!m_beginDrive.isPlaying)
            {
                int indexEngine = (int)Random.Range(0.0f, m_beginDriveClips.Length - 1);
                int indexTracks = (int)Random.Range(0.0f, m_tracksBeginDriveClips.Length - 1);
                m_beginDrive.clip = m_beginDriveClips[indexEngine];
                m_tracksBeginDrive.clip = m_tracksBeginDriveClips[indexTracks];

                m_idleEngine.Stop();
                m_beginDrive.Play();
                m_tracksBeginDrive.Play();
                //m_driving.Play();
            }
           
        }
        if (!Input.GetKeyUp(KeyCode.W))
        {
            m_beginDrive.Stop();
            m_driving.Stop();
            m_endDrive.Play();
            
        }

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    m_endDrive.Stop();
        //    m_beginDrive.Play();
        //    m_driving.Play();
        //}

        //if (Input.GetKeyUp(KeyCode.S))
        //{
        //    m_beginDrive.Stop();
        //    m_driving.Stop();
        //    m_endDrive.Play();
        //}

    }

}
