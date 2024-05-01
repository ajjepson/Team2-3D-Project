using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public PlayerHealth playerHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerHealth.Heal(20);
            Debug.Log("Player Healed");
            Destroy(gameObject);
        }
    }
}
