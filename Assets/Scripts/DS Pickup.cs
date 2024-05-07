using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSPickup : MonoBehaviour
{
    public PlayerCamera playerCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerCamera.hasTwinSmgs = true;
            playerCamera.SwitchToDualSMGs();
            Debug.Log("Added Ammo");
            Destroy(gameObject);
        }
    }
}
