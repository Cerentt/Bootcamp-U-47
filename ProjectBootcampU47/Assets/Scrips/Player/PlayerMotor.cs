using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool isSprinting = false; // Added variable to track sprinting
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public float speed = 5f;
    public float sprintSpeed = 10f; // Speed for sprinting

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = characterController.isGrounded;

        // Check for sprint input
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        // Choose the appropriate speed based on sprinting
        float currentSpeed = isSprinting ? sprintSpeed : speed;

        // Move the character based on input and speed
        characterController.Move(transform.TransformDirection(moveDirection) * currentSpeed * Time.deltaTime);

        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;

        // If the player is grounded and falling, reset the vertical velocity
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // Move the character based on the final velocity
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        // Check if the player is grounded and perform the jump
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}
