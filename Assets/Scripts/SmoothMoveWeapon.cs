using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMoveWeapon : MonoBehaviour
{
    public Transform firstPersonCamera;
    public float smoothRotationSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.position = firstPersonCamera.position;

        // Smoothly rotate to match first person camera's rotation
        Quaternion targetRotation = firstPersonCamera.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothRotationSpeed * Time.deltaTime);
    }
}
