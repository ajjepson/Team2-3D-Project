using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PausePanel;
    public bool isPaused = false;
    
    

    public void UnPause()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1;
        isPaused = true;
    }

// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PausePanel.SetActive(true);
                Time.timeScale = 0;
                isPaused = true;
            }
            else
            {
                PausePanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
            }
        }
    }

    





    


        


    
    

    

    
}
