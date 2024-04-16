using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayAlien : MonoBehaviour
{
    public float detectionRange = 10f;
    public float aggroSpeed = 5f;
    public float strafeDistance = 3f;
    public float shootInterval = 2f;
    public float projectileSpeed = 10f;
    public float stopDistance = 2f; // Distance at which the enemy stops moving towards the player
    public float strafeInterval = 3f;
    public float strafeSpeed = 2f;
    public float maxHealth = 100;
    public GameObject playerCamera;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint; // Shooting point
    public AudioClip aggroSound;
    public AudioClip shootSound;
    public AudioClip hitSound;
    public AudioClip deathSound;
    public Animator animator;

    private Transform player;
    private Rigidbody rb;
    private AudioSource audioSource;
    private float currentHealth;
    private bool isAggro = false;
    private bool isStrafing = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (!isAggro && Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            TriggerAggro();
        }

        if (isAggro)
        {
            // Look at the player's camera only on the Y-axis
            Vector3 lookAtPosition = playerCamera.transform.position;
            lookAtPosition.y = transform.position.y; // Keep the Y position of the enemy unchanged
            transform.LookAt(lookAtPosition);

            // Check distance to player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > stopDistance)
            {
                // Move towards the player
                Vector3 moveDirection = (player.position - transform.position).normalized;
                rb.velocity = moveDirection * aggroSpeed;

                // Play footstep sounds...

                // Shoot projectiles...
                if (Time.time % shootInterval == 0)
                {
                    ShootProjectile();
                }
            }
            else
            {
                rb.velocity = Vector3.zero;
            }

            // Strafe left and right
            if (!isStrafing && Time.time % strafeInterval < 0.1f)
            {
                StartCoroutine(Strafe());
            }
        }
    }

    void TriggerAggro()
    {
        isAggro = true;
        audioSource.PlayOneShot(aggroSound);
        animator.SetTrigger("Aggro");
    }

    void ShootProjectile()
    {
        // Instantiate the projectile at the given point
        GameObject projectileInstance = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        // Set the velocity of the projectile
        Rigidbody projectileRigidbody = projectileInstance.GetComponent<Rigidbody>();
        if (projectileRigidbody != null)
        {
            Vector3 direction = (player.position - projectileSpawnPoint.position).normalized;
            projectileRigidbody.velocity = direction * projectileSpeed;
        }
    }

    IEnumerator Strafe()
    {
        isStrafing = true;
        Vector3 strafeDirection = Random.insideUnitCircle.normalized;
        Vector3 targetPosition = originalPosition + strafeDirection * strafeDistance;
        Vector3 strafeVelocity = (targetPosition - transform.position).normalized * strafeSpeed;
        float startTime = Time.time;
        while (Time.time - startTime < strafeInterval)
        {
            rb.velocity = strafeVelocity;
            yield return null;
        }
        rb.velocity = Vector3.zero;
        isStrafing = false;
    }

    public void TakeDamage(int damage)
    {
        // Damage logic...
    }

    void Die()
    {
        // Death logic...
    }
}
