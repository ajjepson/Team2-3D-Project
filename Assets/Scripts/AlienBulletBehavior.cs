using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBulletBehavior : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
