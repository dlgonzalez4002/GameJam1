using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private int maxJumps;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dashForce;
    private Transform cameraTransform;
    private bool canJump = true;
    private int numJumps = 0;
    private bool canDash = true;
    private bool isDashing = false;
    private bool inAir = false;
    private float playerSpeed = 0f;
    private Rigidbody rb;
    private Vector2 moveInput;

    private Vector3 death = new Vector3(-12.82f, 0.216f, -4.73f);

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        //Debug.Log(rb.linearVelocity.x/playerSpeed);
        // Only cares about the velocities in the x and z direction
        playerSpeed = (float)Math.Sqrt(Math.Pow(rb.linearVelocity.x, 2) + Math.Pow(rb.linearVelocity.z, 2));

        // The player is dashing when they are moving faster than their movespeed
        if (playerSpeed > moveSpeed)
        {
            isDashing = true;
        }

        // !canDash is asking if the player is dashing, false meaning they are
        if (isDashing)
        {
            rb.AddForce(-Mathf.Sign(rb.linearVelocity.x) * 5 / 2, 0, -Mathf.Sign(rb.linearVelocity.z) * 5 / 2);
            if (playerSpeed <= moveSpeed)
            {
                isDashing = false;

                if (!inAir)
                {
                    canDash = true;
                }
            }
            else
            {
                return;
            }
        }


        // Moves the player based on their input
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        // Remove vertical influence
        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        // Convert input into world space based on camera
        Vector3 moveDirection =
        camForward * moveInput.y +
        camRight * moveInput.x;

        rb.AddForce(moveDirection * moveSpeed);

        if (playerSpeed > 0)
        {
            if (moveInput.x == 0 && moveInput.y == 0 && inAir)
            {
                return;
            }

            rb.AddForce(-rb.linearVelocity.x, 0, -rb.linearVelocity.z);
        }
    }

    void OnJump()
    {
        // numJumps < maxJumps allows for multiple jumps should maxJumps > 1
        if (canJump && numJumps < maxJumps)
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            numJumps++;
            AudioManager.Play("Jump");
        }
        else
        {
            canJump = false;
        }
    }

    void OnDash()
    {
        if (canDash)
        {
            canDash = false;
            rb.AddForce(rb.linearVelocity.x / playerSpeed * dashForce, 0, rb.linearVelocity.z / playerSpeed * dashForce, ForceMode.Impulse);
        }
    }

    void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // If the player hits the ground, resets their jump cycle
        if (collision.gameObject.CompareTag("Ground"))
        {
            inAir = false;

            canJump = true;
            numJumps = 0;

            canDash = true;
        }

        if (collision.gameObject.CompareTag("Evil"))
        {
            transform.position = death;
            AudioManager.Play("Death");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // When the player leaves the ground they are in the air
        if (collision.gameObject.CompareTag("Ground"))
        {
            inAir = true;
        }
    }
    
}
