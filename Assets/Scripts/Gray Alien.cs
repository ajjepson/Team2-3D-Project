using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayAlien : MonoBehaviour
{
    public float aggroRange = 10f;
    public float aggroCooldown = 3f;
    public float movementSpeed = 5f;
    public float rotationSpeed = 3f;
    public float strafeDistance = 2f;
    public float shootCooldown = 1f;
    public int maxHealth = 100;

    private Transform player;
    private float lastAggroTime;
    private float lastShootTime;
    private int currentHealth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            ;
            AttackPlayer();
        }
        else
        {
            // Movement logic when not aggroed
        }
    }

    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            Debug.Log("Correct distance for aggro");
            RaycastHit hit;
            LayerMask layerMask = ~(1 << LayerMask.NameToLayer("Enemy")); // Ignore layer with Enemy tag
            if (Physics.Linecast(transform.position, player.position, out hit, layerMask))
            {
                if (hit.transform == player)
                    return true;
                Debug.Log("Enemy can see the player");
            }
        }
        return false;
    }

    void AttackPlayer()
    {
        // Rotate towards player
        Vector3 direction = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        // Movement towards player
        transform.position += transform.forward * movementSpeed * Time.deltaTime;

        // Strafing
        transform.position += transform.right * Mathf.Sin(Time.time) * strafeDistance * Time.deltaTime;

        // Shooting
        if (Time.time - lastShootTime > shootCooldown)
        {
            lastShootTime = Time.time;
            Shoot();
        }
    }

    void Shoot()
    {
        // Shooting logic here
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Death logic here
    }
}
