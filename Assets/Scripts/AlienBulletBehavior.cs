using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBulletBehavior : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f);
    }

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
