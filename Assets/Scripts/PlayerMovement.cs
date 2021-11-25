using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private float speed = 12f;
    private float gravity = -9.81f;
    private float jumpHeight = 3f;
    private Transform groundCheck;
    private float groundDistance = 0.4f;
    private LayerMask groundMask;
    private Vector3 velocity;
    private bool isGrounded;
    public void Update()
    {
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight *-2f * gravity);
            print("Hello");
        }
        if(Input.GetButtonDown("Jump"))
        {
            print("JUMP");
        }
        if(isGrounded)
        {
            print("isGrounded");
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}


