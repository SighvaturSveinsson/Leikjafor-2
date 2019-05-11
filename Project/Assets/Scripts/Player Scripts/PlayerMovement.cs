using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController character_controller;
    private Vector3 move_direction;

    public float speed = 5f;
    private float gravity = 20f;

    public float jump_force = 10f;
    private float vertical_Velocity;

    void Awake()
    {
        character_controller = GetComponent<CharacterController>();
    }

    void Jump()
    {
        // If player is grounded and space is pressed
        if(character_controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertical_Velocity = jump_force;
        }
    }

    // Function that moves the player
    void MovePlayer()
    {
        // Gets the x and y posistion of the player
        move_direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        // 
        move_direction = transform.TransformDirection(move_direction);
        // Multiply by deltatime to smooth out movement
        move_direction *= speed * Time.deltaTime;

        ApplyGravity();

        // Moves the player
        character_controller.Move(move_direction);
    }
    // Applies gravity
    void ApplyGravity()
    {
        
        // Checks if character is grounded
        if (character_controller.isGrounded)
        {
            // Applies gravity even when the player is grounded
            vertical_Velocity -= gravity * Time.deltaTime;

            Jump();
        }
        else
        {
            // Applies gravity
            vertical_Velocity -= gravity * Time.deltaTime;
        }

        move_direction.y = vertical_Velocity * Time.deltaTime;

    }

    

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ApplyGravity();
    }
}
