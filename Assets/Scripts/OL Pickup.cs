using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLPickup : MonoBehaviour
{
    public PlayerCamera playerCamera;
    public PlayerShOL playerShOL;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerCamera.hasOrbLauncher = true;
            playerCamera.SwitchToOrbLauncher();
            playerShOL.AddOrbLauncherAmmo(10);
            Destroy(gameObject);
        }
    }
}
