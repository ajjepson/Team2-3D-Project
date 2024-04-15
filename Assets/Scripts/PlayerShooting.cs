using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Animator anim;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 30f;
    public float fireRate = 0.1f;
    public int maxAmmo = 100;

    public TMP_Text ammoText;

    public AudioClip[] bolterShootSounds;
    public AudioClip emptyAmmoSound;

    public Quaternion handAdjustedRotation = Quaternion.Euler(90f, 90f, 90f);
    private int currentAmmo = 40;
    private float nextFireTime;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ammoText.text = $"{currentAmmo}";
    }

    private void Update()
    {
        // Check if it's time to fire and if there's enough ammo
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && currentAmmo > 0)
        {
            nextFireTime = Time.time + fireRate;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation * handAdjustedRotation);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;

            anim.Play("Fire");

            currentAmmo--;
            int randomIndex = Random.Range(0, bolterShootSounds.Length);
            audioSource.PlayOneShot(bolterShootSounds[randomIndex]);
            Destroy(bullet, 3f);
            ammoText.text = $"{currentAmmo}";
        }
        else if (Input.GetMouseButtonDown(0) && currentAmmo <= 0)
        {
            // Play empty ammo sound
            audioSource.PlayOneShot(emptyAmmoSound);
        }

        if (currentAmmo <= 0)
        {
            ammoText.color = Color.red;
        }
    }

    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    
}
