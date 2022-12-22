using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FirstPersonController : MonoBehaviour
{
    private CharacterController controller;
    public Animator animator;

    //Audio variables
    [Header("Audio")]
    public AudioSource walkingAudio;
    public AudioSource jumpingAudio;

    //Player movement variables
    [Header("Movement")]
    [SerializeField] private float speed = 7f;
    private Vector3 moveDirection;
    //Camera variables
    private Transform cam;

    //Gravity variables
    [SerializeField] private Vector3 velocity;
    private float gravity = -19.62f;

    //Ground check variables
    [Header("Ground check")]
    public Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask ground;
    public bool isGrounded;

    //Jump variables
    private float jumpHeight = 3f;

    //Crouch variables
    [Header("Crouch")]
    public bool isCrouched;
    private Vector3 crouchHeight = new Vector3(0f, 0.9f, 0f);
    [SerializeField] private float crouchSpeed = 2f;

    //Ceiling check variables
    [Header("Ceiling check")]
    public Transform ceilingCheck;
    private float ceilingDistance = 0.4f;
    public LayerMask ceiling;
    public bool ceilingDetected;

    void Start()
    {
        //Component references
        controller = GetComponent<CharacterController>();
        //animator = GetComponentInChildren<Animator>();
        cam = Camera.main.transform;

        //Hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        //Function call
        PlayerMovement();
        Crouch();

        //Checks if the player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Checks if there is a ceiling above the player
        ceilingDetected = Physics.CheckSphere(ceilingCheck.position, ceilingDistance, ceiling);

        //Jump
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            jumpingAudio.Play();

            //Plays jump animation when the player jumps
            if (velocity.y > 0)
            {
                animator.SetBool("IsJumping", true);
            }
        }
        //Transition back to idle animation from jump animation when the player's velocity is -2f
        if (velocity.y == -2f)
        {
            animator.SetBool("IsJumping", false);
        }

        //Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    //Player movement function
    private void PlayerMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveDirection = (cam.transform.forward * vertical + cam.transform.right * horizontal).normalized;
        moveDirection.y = 0f;

        controller.Move(moveDirection * speed * Time.deltaTime);

        //Plays running animation when the player moves
        if (moveDirection.x != 0 && moveDirection.z != 0)
        {
            animator.SetBool("IsMoving", true);

            if (!walkingAudio.isPlaying && isGrounded)
            {
                walkingAudio.Play();
            }

        }
        else
        {
            animator.SetBool("IsMoving", false);
            if (walkingAudio.isPlaying || !isGrounded)
            {
                walkingAudio.Stop();
            }
        }
    }

    //Crouch function
    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && !isCrouched)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchHeight.y, transform.localScale.z);
            isCrouched = true;
            controller.height = 1f;
            controller.center = new Vector3(0f, -0.27f, 0f);
            speed = crouchSpeed;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift) && isCrouched && isGrounded && !ceilingDetected)
        {
            transform.localScale = new Vector3(transform.localScale.x, 1.8f, transform.localScale.z);
            isCrouched = false;
            controller.height = 2f;
            controller.center = new Vector3(0f, 0f, 0f);
            speed = 7f;
        }
    }
}
