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


    private void Start()
    {
        controlPanel.SetActive(false); //default
    }

    //Buttons for scene and panel loading//
    public void StartGame()
    {
        /*SceneManager.LoadScene("level one");*/
        StartCoroutine(ChangeScene());
    }

    public void ControlPanel()
    {
        controlPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void MainMenuPanel()
    {
        mainPanel.SetActive(true);
        controlPanel.SetActive(false);
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
