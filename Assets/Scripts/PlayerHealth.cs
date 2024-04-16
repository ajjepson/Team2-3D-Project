using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public AudioClip takingDamageSound;
    public AudioClip deathSound;
    public float restartDelay = 3f;
    public TMP_Text healthText;
    public GameObject gameOverText;

    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        healthText.text = currentHealth.ToString() + "%";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyOrbProjectile"))
        {
            TakeDamage();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("DeathZone"))
        {
            Die();
        }
    }

    void TakeDamage()
    {
        currentHealth -= 20; // Adjust the damage amount as needed

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            if (takingDamageSound != null)
            {
                audioSource.PlayOneShot(takingDamageSound);
            }
        }
    }

    void Die()
    {
        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        gameOverText.SetActive(true);
        StartCoroutine(RestartAfterDelay());
    }

    IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
