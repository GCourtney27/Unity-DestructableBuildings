using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : Singleton<Game>
{
    [Header("Game Properties")]
    [SerializeField] float m_destructionProggress = 0.0f;
    [SerializeField] Transform m_startCameraPos = null;
    [SerializeField] Transform m_playerCameraPos = null;
    public bool isDebugModeActive = true;

    [Space]
    [Header("UI")]
    [SerializeField] TextMeshProUGUI m_destructionProggressUI = null;
    [SerializeField] TextMeshProUGUI m_endGameTextUI = null;
    [SerializeField] TextMeshProUGUI m_beginGameTextUI = null;
    [SerializeField] TextMeshProUGUI m_timerUI = null;
    [SerializeField] GameObject m_gamePanel = null;
    
    [Space]
    [Header("Audio")]
    [SerializeField] AudioSource m_backgroundMusic = null;

    [Space]
    [Header("Round Music")]
    [SerializeField] AudioClip[] m_roundClips = null;
    [SerializeField] AudioSource m_blankAudioSource = null;

    Camera mainCamera = null;
    bool isGameOver = false;
    bool gameHasStarted = false;
    float startDelay = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        startDelay = timeLeft - 5;
        mainCamera = Camera.main;
        mainCamera.transform.position = m_startCameraPos.position;
        mainCamera.transform.rotation = m_startCameraPos.rotation;
    }
    
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && !gameHasStarted)
        {
            gameHasStarted = true;
            mainCamera.transform.position = m_playerCameraPos.position;
            mainCamera.transform.rotation = m_playerCameraPos.rotation;
            if(!isDebugModeActive) PlayBackgroundMusic();
            Camera.main.GetComponent<CamMouseLook>().enabled = true;
            PlayerManager.instance.player.GetComponent<CharacterContoller>().enabled = true;
        }

        if (gameHasStarted && !isDebugModeActive)
        {
            UpdateGameSettings();
        }
    }

    void UpdateGameSettings()
    {
        if (m_destructionProggress >= 100.0f)
        {
            isGameOver = true;
            StartCoroutine(Fade(false));
        }
        RoundTimer();

        if (timeLeft > startDelay)
        {
            m_beginGameTextUI.text = "You have " + (int)timeLeft + " seconds to destroy everything. You were never here.\n --. --- --- -.. / .-.. ..- -.-. -.-";
        }
        else
        {
            m_beginGameTextUI.text = "";
        }
    }

    public float timeLeft = 7.0f;
    void RoundTimer()
    {
        m_timerUI.text = ((int)timeLeft).ToString() + "s";
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            isGameOver = true;
            timeLeft = 0.0f;
            StartCoroutine(Fade(true));
        }
    }

    public IEnumerator Fade(bool failed)
    {

        Image launchPanel = m_gamePanel.GetComponent<Image>();
        while (launchPanel.color.a < 255)
        {
            launchPanel.color = new Color(launchPanel.color.r, launchPanel.color.g, launchPanel.color.b, launchPanel.color.a + Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        if (failed)
        {
            m_endGameTextUI.text = "You did not destroy the base in time.";
        }
        else if (!failed)
        {
            m_endGameTextUI.text = "You have destroyed all evidence you were here.";
        }

    }

    public void AddToDestructionPoints(float points)
    {
        if(!isGameOver)
        {
            m_destructionProggress += points;
            m_destructionProggressUI.text = "Destruction Progress: " + m_destructionProggress.ToString();
        }
    }

    void PlayBackgroundMusic()
    {
        m_backgroundMusic.Play();
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
