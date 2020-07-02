using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FPSGame : MonoBehaviour
{
    [SerializeField] GameObject m_gamePanel = null;
    [SerializeField] TextMeshProUGUI m_EndGameText = null;
    [SerializeField] float m_endGameDelay = 1.0f;
    float m_endGameTimer = 0.0f;
    bool NavScene = false;
    void Start()
    {
        m_endGameTimer = m_endGameDelay;
    }
    
    void Update()
    {
        if(waitDelay() && NavScene)
        {
            SceneManager.LoadScene("SolarSystem");
        }

    }

    bool waitDelay()
    {
        m_endGameTimer -= Time.deltaTime;
        if (m_endGameTimer <= 0.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartFailedScreen(bool failed)
    {
        StartCoroutine(Fade(failed));
    }

    public IEnumerator Fade(bool failed)
    {
        Image launchPanel = m_gamePanel.GetComponent<Image>();
        Debug.Log(launchPanel.color.a);
        while (launchPanel.color.a < 255)
        {
            launchPanel.color = new Color(launchPanel.color.r, launchPanel.color.g, launchPanel.color.b, launchPanel.color.a + Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        if(failed)
        {
            m_EndGameText.text = "Mission Failed. Target has fled to safety.";
        }
        else if(!failed)
        {
            m_EndGameText.text = "Mission Success. Target eliminated.";
            NavScene = true;
        }

    }

    

}
