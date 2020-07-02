using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] AudioSource m_backgroundMusic = null;
    [SerializeField] GameObject m_launchPanel = null;
    [SerializeField] GameObject m_menuPanel = null;
    [SerializeField] GameObject m_controlsPanel = null;
    [SerializeField] Transform m_cameraTarget1 = null;
    [SerializeField] Transform m_cameraTarget2 = null;
    void Start()
    {
        //Lower alpha of LaunchPanel(Take instance of panel?)
        //float launchPanelAlpha = m_launchPanel.GetComponent<Image>().color.a;
        // launchPanelAlpha--;
        // m_launchPanel.GetComponent<Image>().color.a = ;
        StartCoroutine(Fade());
    }
    
    void Update()
    {
        
    }

    public IEnumerator Fade()
    {
        Image launchPanel = m_launchPanel.GetComponent<Image>();
            Debug.Log(launchPanel.color.a);
        while (launchPanel.color.a > 0)
        {
            launchPanel.color = new Color(launchPanel.color.r, launchPanel.color.g, launchPanel.color.b, launchPanel.color.a - Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public void LoadScene(string sceneName)
    {
        Cursor.visible = false;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ClickToBegin()
    {
        m_launchPanel.SetActive(false);
        m_menuPanel.SetActive(true);
        Image launchPanel = m_launchPanel.GetComponent<Image>();
        m_launchPanel.GetComponent<Image>().color = new Color(launchPanel.color.r, launchPanel.color.g, launchPanel.color.b, 1);

        Camera.main.transform.position = m_cameraTarget2.position;
        Camera.main.transform.rotation = m_cameraTarget2.rotation;
        Camera.main.GetComponent<OrbitAround>().m_centerPoint = m_cameraTarget2;
    }

    public void ViewControls()
    {
        m_menuPanel.SetActive(false);
        m_controlsPanel.SetActive(true);
    }

    public void ControlsGoBack()
    {
        m_menuPanel.SetActive(true);
        m_controlsPanel.SetActive(false);
    }

    public void GoBack()
    {
        m_launchPanel.SetActive(true);
        StartCoroutine(Fade());
        m_menuPanel.SetActive(false);

        Camera.main.transform.position = m_cameraTarget1.position;
        Camera.main.transform.rotation = m_cameraTarget1.rotation;
        Camera.main.GetComponent<OrbitAround>().m_centerPoint = m_cameraTarget1;
    }

}
