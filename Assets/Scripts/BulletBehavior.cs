using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private bool hasHit = false;

    public AudioClip[] bulletImpactSounds;
    public AudioSource audioSource;

    private void Start()
    {
        Destroy(gameObject, 6f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        else
        {
            if (!hasHit)
            {
                // Disable movement by removing Rigidbody component
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Destroy(rb);
                }

                // Disable collision by removing Collider component
                Collider col = GetComponent<Collider>();
                if (col != null)
                {
                    Destroy(col);
                }

                // Disable the mesh renderer to hide the visual representation
                MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.enabled = false;
                }

                // Set the flag to prevent further processing of OnCollisionEnter
                hasHit = true;

                // Optionally, you can add sound emission logic here
                // For example, play a sound
                AudioSource audioSource = GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.Play();
                }

                audioSource.enabled = true;
                int randomIndex = Random.Range(0, bulletImpactSounds.Length);
                audioSource.PlayOneShot(bulletImpactSounds[randomIndex]);
            }
        }
    }
}
