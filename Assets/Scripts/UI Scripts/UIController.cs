using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public CanvasGroup canvasgroup;
    public bool fadein = false;
    public bool fadeout = false;
    public float TimeToFade;

    public GameObject mainPanel;
    public GameObject controlPanel;
    public GameObject creditsPanel;

    private void Start()
    {
        controlPanel.SetActive(false); //default
        creditsPanel.SetActive(false); //default
        Time.timeScale = 1;

    }

    //Buttons for scene and panel loading//
    public void StartGame()
    {
        
        StartCoroutine(ChangeScene());
    }

    public void MainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void ControlPanel()
    {
        controlPanel.SetActive(true);
        mainPanel.SetActive(false);
    }
    public void CreditsPanel()
    {
        creditsPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void MainMenuPanel()
    {
        mainPanel.SetActive(true);
        controlPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }

    

    private void Update()
    {
        if(fadein == true)
        {
            if (canvasgroup.alpha < 1)
            {
                canvasgroup.alpha += TimeToFade * Time.deltaTime;
                if(canvasgroup.alpha >= 1)
                {
                    fadein = false;
                }
            }
        }
        if (fadeout == true)
        {
            if (canvasgroup.alpha >= 0)
            {
                canvasgroup.alpha -= TimeToFade * Time.deltaTime;
                if (canvasgroup.alpha == 0)
                {
                    fadeout = false;
                }
            }
        }
    }

    public void FadeIn()
    {
        fadein = true;
    }

    public void FadeOut()
    {
        fadeout = true;
    }

    public IEnumerator ChangeScene()
    {
        FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("level one");
    }

   
}
