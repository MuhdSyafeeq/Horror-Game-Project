using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Character Controller")]
    public AnimationCharacter ac;
    public CharacterController controller;
    public float speed = 5f;

    [Space(5)]
    [Header("Gravity Physics")]
    public float gravity = -9.81f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    [Space(5)]
    [Header("Jump Physics")]
    public float jumpHeight = 3f;

    [Space(5)]
    [Header("Audio")]
    public AudioSource audio;
    public AudioClip audioRun;
    public AudioClip audioWalk;

    // Data Test Areas
    //public float HorizontalF;
    //public float VerticalF;

    // Update is called once per frame
    void Update()
    {
        if(gameManager.Instance.isPaused != true)
        {
            // Check ground
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0) { velocity.y = -2f; }
            
            // Move Inputs
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

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

            if( isGrounded == true && audio.isPlaying == false)
            {
                if( ((ac.velocityX > 0.0f && ac.velocityX  < 0.55f) || (ac.velocityX  < 0.0f && ac.velocityX  > -0.55f)) 
                    || ((ac.velocityZ > 0.0f && ac.velocityZ < 0.55f) || (ac.velocityZ < 0.0f && ac.velocityZ > -0.55f)) )
                {
                    audio.clip = audioWalk;
                    audio.Play();
                }
                else if( ((ac.velocityX  > 0.55f && ac.velocityX  < 2.05f) || (ac.velocityX  < 0.0f && ac.velocityX  > -2.05f))
                    || ((ac.velocityZ > 0.55f && ac.velocityZ < 2.05f) || (ac.velocityZ < 0.0f && ac.velocityZ > -2.05f)) )
                {
                    audio.clip = audioRun;
                    audio.Play();
                }
            }
        }
    }
}
