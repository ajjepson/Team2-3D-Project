using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public AudioClip[] bulletImpactSounds;

    public AudioSource audioSource;

    private void Start()
    {
        Destroy(gameObject, 4f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        else
        {
            audioSource.enabled = true;
            int randomIndex = Random.Range(0, bulletImpactSounds.Length);
            audioSource.PlayOneShot(bulletImpactSounds[randomIndex]);
            Destroy(gameObject);
        }

        Destroy(gameObject, 4f);
    }
}
