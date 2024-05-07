using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Stored Sprint Multiplier is for the code. Sprint multiplier is for the inspector.
    public float moveSpeed;
    public float sprintMultiplier = 2.5f;
    private float storedSprintMultiplier = 1.0f;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    // Footstep Stuff

    public AudioClip[] footstepSounds; // Array of footstep audio clips
    private float footstepInterval = 0.8f; // Interval between footstep sounds
    public AudioSource audioSource; // Reference to the AudioSource component

    private float lastFootstepTime; // Time of the last footstep sound

    // Keybinds

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void MyInput()
    {
        if (GlobalVariables.playerAlive)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            // When to jump
            if (Input.GetKeyDown(jumpKey) && readyToJump && grounded)
            {
                readyToJump = false;

                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }

            // Sprinting
            if (Input.GetKey(sprintKey))
            {
                storedSprintMultiplier = sprintMultiplier;
                footstepInterval = 0.5f;
            }
            else
            {
                storedSprintMultiplier = 1.0f;
                footstepInterval = 0.8f;
            }
        }
    }

    private void MovePlayer()
    {
        if (GlobalVariables.playerAlive)
        {
            // Calculate movement direction
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * storedSprintMultiplier, ForceMode.Force);

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    //Footsteps
                    if (Time.time - lastFootstepTime >= footstepInterval)
                    {
                        // Play a random footstep sound
                        PlayRandomFootstepSound();
                        // Update the time of the last footstep sound
                        lastFootstepTime = Time.time;
                    }
                }
            }
            else
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * storedSprintMultiplier * airMultiplier, ForceMode.Force);
            }
        }
            
    }

    void PlayRandomFootstepSound()
    {
        // Choose a random footstep sound from the array
        int randomIndex = Random.Range(0, footstepSounds.Length);
        AudioClip footstepSound = footstepSounds[randomIndex];
        float randomVolume = Random.Range(0.3f, 0.5f);

        // Play the selected footstep sound
        audioSource.PlayOneShot(footstepSound, randomVolume);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * storedSprintMultiplier * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // Reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.5f, whatIsGround);

        MyInput();
        SpeedControl();

        // Handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("LevelOneTransition"))
            {
                SceneManager.LoadScene("Level two");
            }
        }
    }
}
