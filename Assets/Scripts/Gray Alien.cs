using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayAlien : MonoBehaviour
{
    public float detectionRange = 10f;
    public float aggroSpeed = 5f;
    public float strafeDistance = 3f;
    public float shootInterval = 2f;
    public float projectileSpeed = 25f;
    public float lastShotTime = 0f;
    public float stopDistance = 2f; // Distance at which the enemy stops moving towards the player
    public float strafeInterval = 3f;
    public float strafeSpeed = 2f;
    public float maxHealth = 100;
    public GameObject playerCamera;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint; // Shooting point
    public AudioClip[] aggroSounds;
    public AudioClip[] shootSounds;
    public AudioClip[] hitSounds;
    public Animator animator;

    private Transform player;
    private Rigidbody rb;
    private AudioSource audioSource;
    private float currentHealth;
    private bool isAggro = false;
    private bool isStrafing = false;
    private bool isDead = false;
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
        if (!isDead && !isAggro && Vector3.Distance(transform.position, player.position) < detectionRange)
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

                // Shoot
                if (Time.time - lastShotTime >= shootInterval && !isDead)
                {
                    ShootProjectile();
                    lastShotTime = Time.time; // Update the last shot time
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
        int randomIndex = Random.Range(0, aggroSounds.Length);
        audioSource.PlayOneShot(aggroSounds[randomIndex]);
        // animator.SetTrigger("Aggro");
    }

    void ShootProjectile()
    {
        GameObject projectileInstance = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Rigidbody projectileRigidbody = projectileInstance.GetComponent<Rigidbody>();

        if (projectileRigidbody != null)
        {
            Vector3 direction = (playerCamera.transform.position - projectileSpawnPoint.position).normalized;
            projectileRigidbody.velocity = direction * projectileSpeed;
        }
        
        // Enemy Shoot SFX
        int randomIndex = Random.Range(0, shootSounds.Length);
        audioSource.PlayOneShot(shootSounds[randomIndex]);
    }

    IEnumerator Strafe()
    {
        isStrafing = true;
        Vector3 strafeDirection = Random.insideUnitCircle.normalized;
        strafeDirection.y = 0f; // Ensure strafing only occurs along the XZ plane
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BolterProjectile") && !isDead)
        {
            Debug.Log("Enemy took damage");
            TakeDamage(40f); // Call TakeDamage method when colliding with a BolterProjectile
            Destroy(other.gameObject); // Destroy the projectile upon collision
        }
    }

    public void TakeDamage(float damage)
    {
        isAggro = true;
        currentHealth -= damage; // Decrease current health by damage amount
        int randomIndex = Random.Range(0, hitSounds.Length);
        audioSource.PlayOneShot(hitSounds[randomIndex]);

        if (currentHealth <= 0)
        {
            StartCoroutine(DieCoroutine()); // Call Die method if health is zero or less
        }
    }

    IEnumerator DieCoroutine()
    {
        isDead = true; isAggro = false;

        // Trigger death animation
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        // Wait for destroyDelay seconds
        yield return new WaitForSeconds(5);

        // Destroy the enemy object
        Destroy(gameObject);
    }
}
