using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolterAmmoPickup : MonoBehaviour
{
    public PlayerShooting playerShooting;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerShooting.AddBolterAmmo(10);
            Debug.Log("Added Ammo");
            Destroy(gameObject);
        }
    }
}
