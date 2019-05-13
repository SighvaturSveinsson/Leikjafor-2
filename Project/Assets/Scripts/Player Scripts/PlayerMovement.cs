using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Sækir CharacterController component
    private CharacterController character_controller;
    // Vector 3 breyta
    private Vector3 move_direction;
    // Hraði fyrir player og þyngdarafl
    public float speed = 5f;
    private float gravity = 20f;
    // Hopp kraftur
    public float jump_force = 10f;
    private float vertical_Velocity;
    // Awake er það fyrsta sem keyrir þegar leikurinn byrjar
    void Awake()
    {
        character_controller = GetComponent<CharacterController>();
    }
    // Function til að hoppa
    void Jump()
    {
        // Ef player er á jörðinni og það er ýtt á space
        if (character_controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertical_Velocity = jump_force;
        }
    }
    // Function sem hreyfir player
    void MovePlayer()
    {
        // Fær x and y staðsetningu af playerinum
        move_direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        // Uppfærir x og y staðsetningu
        move_direction = transform.TransformDirection(move_direction);
        // Vegna þess að Update er called einu sinni per frame þarf að margfalda með deltatime til að playerinn færist á sama hraða sama hvað þú ert með mikið FPS(Frames Per Second)
        move_direction *= speed * Time.deltaTime;

        ApplyGravity();

        // Færir player
        character_controller.Move(move_direction);
    }
    // Þyngdarafl
    void ApplyGravity()
    {

        // Vegna þess að Update er called einu sinni per frame þarf að margfalda með deltatime til að playerinn færist á sama hraða sama hvað þú ert með mikið FPS(Frames Per Second)
        vertical_Velocity -= gravity * Time.deltaTime;
        Jump();
        move_direction.y = vertical_Velocity * Time.deltaTime;
    }



    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }
}



































