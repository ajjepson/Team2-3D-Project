using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplier;
    [SerializeField] private float bobbingAmount = 0.1f;
    [SerializeField] private float bobbingSpeed = 6f;
    [SerializeField] private float bobbingSmoothness = 4f;

    private Vector3 initialPosition;
    private Vector3 targetBobbingPosition;
    private Vector3 bobbingVelocity;
    private float currentBobbingAmount = 0f;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.playerAlive)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
            float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

            // Calculate target rotation
            Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
            Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

            Quaternion targetRotation = rotationX * rotationY;

            // Rotate
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);

            // Weapon bobbing when walking
            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
                // Gradually increase bobbing amount
                currentBobbingAmount = Mathf.Lerp(currentBobbingAmount, bobbingAmount, bobbingSmoothness * Time.deltaTime);

                // Calculate bobbing offset
                float bobbingY = Mathf.Sin(Time.time * bobbingSpeed) * currentBobbingAmount;
                float bobbingX = Mathf.Cos(Time.time * bobbingSpeed * 2) * currentBobbingAmount * 0.5f; // Horizontal bobbing

                targetBobbingPosition = initialPosition + new Vector3(bobbingX, bobbingY, 0f);

                // Smoothly move to the target bobbing position
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetBobbingPosition, ref bobbingVelocity, bobbingSmoothness * Time.deltaTime);
            }
            else
            {
                // Reset bobbing amount when not moving
                currentBobbingAmount = 0f;

                // Reset position when not moving
                transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, bobbingSmoothness * Time.deltaTime);
            }
        }
    }
}
