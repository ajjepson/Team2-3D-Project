using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public AudioClip takingDamageSound;
    public AudioClip beingHealedSound;
    public AudioClip[] playerDeathSounds;
    public float restartDelay = 3f;
    public TMP_Text healthText;
    public GameObject gameOverText;

    public AudioSource audioSource;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();

        GlobalVariables.playerAlive = true;
    }

    private void Update()
    {
        healthText.text = currentHealth.ToString() + "%";
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isDead)
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
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        audioSource.PlayOneShot(beingHealedSound);
    }

    void TakeDamage()
    {
        currentHealth -= 20; // Adjust the damage amount as needed

        if (currentHealth <= 0 && !isDead)
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
        isDead = true;
        GlobalVariables.playerAlive = false;

        int randomIndex = Random.Range(0, playerDeathSounds.Length);
        audioSource.PlayOneShot(playerDeathSounds[randomIndex]);

        gameOverText.SetActive(true);
        StartCoroutine(RestartAfterDelay());
    }

    IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
