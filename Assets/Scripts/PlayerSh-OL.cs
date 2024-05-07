using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShOL : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Animator anim;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 30f;
    public float fireRate = 0.1f;
    public int maxAmmo = 100;

    public TMP_Text ammoText;

    public AudioClip[] orbLauncherShootSounds;
    public AudioClip emptyAmmoSound;
    public AudioClip ammoPickupSound;

    public Quaternion handAdjustedRotation = Quaternion.Euler(90f, 90f, 90f);
    public int currentOrbLauncherAmmo = 40;
    private float nextFireTime;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ammoText.text = $"{currentOrbLauncherAmmo}";
        if (GlobalVariables.playerAlive)
        {
            // Check if it's time to fire and if there's enough ammo
            if (Input.GetMouseButton(0) && Time.time >= nextFireTime && currentOrbLauncherAmmo > 0)
            {
                nextFireTime = Time.time + fireRate;
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation * handAdjustedRotation);
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;

                anim.Play("Fire");

                currentOrbLauncherAmmo--;
                int randomIndex = Random.Range(0, orbLauncherShootSounds.Length);
                audioSource.PlayOneShot(orbLauncherShootSounds[randomIndex]);
                Destroy(bullet, 3f);
                ammoText.text = $"{currentOrbLauncherAmmo}";
            }
            else if (Input.GetMouseButtonDown(0) && currentOrbLauncherAmmo <= 0)
            {
                // Play empty ammo sound
                audioSource.PlayOneShot(emptyAmmoSound);
            }
        }
            

            if (currentOrbLauncherAmmo <= 0)
            {
                ammoText.color = Color.red;
            }
    }

    public void AddOrbLauncherAmmo(int amount)
    {
        currentOrbLauncherAmmo = Mathf.Min(currentOrbLauncherAmmo + amount, maxAmmo);
        audioSource.PlayOneShot(ammoPickupSound);
    }

    public int GetCurrentOrbLauncherAmmo()
    {
        return currentOrbLauncherAmmo;
    }
}
