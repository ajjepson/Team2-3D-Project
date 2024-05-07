using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;

public class LevelTwoTransition : MonoBehaviour
{
    public GameObject youWinText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            youWinText.SetActive(true);
            Invoke("LoadMain", 3.0f);
        }
    }

    private void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }
}
