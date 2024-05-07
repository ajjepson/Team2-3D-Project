using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMovement : MonoBehaviour
{
    public float degrees = 10f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public float rotationSpeed = 80f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    private void Start()
    {
        posOffset = transform.position;
    }

    private void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
