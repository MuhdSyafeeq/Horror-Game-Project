using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;

    //Gravity Implementation Variable
    public float gravity = -9.81f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    // Animation
    public Animator anim;

    //Jump Implementation Variable
    public float jumpHeight = 3f;

    // Data Test Areas
    public float HorizontalF;
    public float VerticalF;

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isPaused != true)
        {
            // Check ground
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0) { velocity.y = -2f; }

            // Move Inputs
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            anim.SetFloat("inputH", Mathf.Abs(x));
            anim.SetFloat("inputV", Mathf.Abs(z));

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);

            //Jump Method
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            //Gravity Method
            if (!isGrounded) { velocity.y += gravity * Time.deltaTime; }
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
