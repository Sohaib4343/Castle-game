using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FirstPersonController : MonoBehaviour
{
    private CharacterController controller;

    //Animator variables
    [Header("Animator")]
    public Animator handAnimator;
    public Animator cameraAnimator;

    //Audio variables
    [Header("Audio")]
    public AudioSource walkingAudio;
    public AudioSource jumpingAudio;

    //Player movement variables
    [Header("Movement")]
    [SerializeField] private float speed = 7f;
    private Vector3 moveDirection;
    public bool isMoving;
    public float drag;
    //Camera variables
    private Transform cam;

    //Gravity variables
    [SerializeField]private Vector3 velocity;
    private float gravity = -19.62f;

    //Ground check variables
    [Header("Ground check")]
    public Transform groundCheck;
    private float groundDistance = 0.8f;
    public LayerMask ground;
    public bool isGrounded;

    //Jump variables
    private float jumpHeight = 3f;
    public bool isJumping;

    //Crouch variables
    [Header("Crouch")]
    public bool isCrouched;
    private Vector3 crouchHeight = new Vector3(0f, 0.9f, 0f);
    [SerializeField] private float crouchSpeed = 2f;

    //Ceiling check variables
    [Header("Ceiling check")]
    public Transform ceilingCheck;
    private float ceilingDistance = 0.8f;
    public LayerMask ceiling;
    public bool ceilingDetected;

    //Script Refrences variables
    private WeaponPickAndDrop weaponPickAndDrop;

    private void Awake()
    {
        //References
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;

        //Script Refrences
        weaponPickAndDrop = GetComponent<WeaponPickAndDrop>();
    }

    void Start()
    {
        //Hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        //Function call
        PlayerMovement();
        Crouch();
        PlayerLandingFunction();
        PlaySwordJumpAnimation();

        //Checks if the player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            isJumping = false;
        }

        //Checks if there is a ceiling above the player
        ceilingDetected = Physics.CheckSphere(ceilingCheck.position, ceilingDistance, ceiling);

        //Jump
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouched)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            jumpingAudio.Play();
            isJumping = true;

            //Plays jump animation when the player jumps
            if (velocity.y > 0)
            {
                //handAnimator.SetBool("IsJumping", true);
            }
        }
        //Transition back to idle animation from jump animation when the player's velocity is -2f
        if (velocity.y == -2f)
        {
            handAnimator.SetBool("IsJumping", false);
        }

        if (!isGrounded)
        {
            isJumping = true;
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

        moveDirection = (cam.transform.forward * vertical + cam.transform.right * horizontal).normalized * drag;
        moveDirection.y = 0f;

        controller.Move(moveDirection * speed * 1f * Time.deltaTime);

        //Plays running animation when the player moves
        if (moveDirection.x != 0 && moveDirection.z != 0)
        {
            //handAnimator.SetBool("IsMoving", true);
            cameraAnimator.SetBool("IsWalking", true);
            handAnimator.SetBool("SwordMove", true);
            isMoving = true;

            if (isMoving)
            {
                if (!walkingAudio.isPlaying && isGrounded) //Play the waling audio when the walkingAudio is not playing and the player is grounded
                {
                    walkingAudio.Play();
                }
            }
        }
        else
        {
            //handAnimator.SetBool("IsMoving", false);
            cameraAnimator.SetBool("IsWalking", false);
            handAnimator.SetBool("SwordMove", false);

            isMoving = false;
            
            if (!isMoving || isJumping) //Stop playing the walking audio when the player is not moving or the player is jumping
            {
                if (walkingAudio.isPlaying && isGrounded)
                {
                    walkingAudio.Stop();
                }
            }
        }
        if (isMoving && isJumping && !isGrounded) //Stop playing the walking audio when the player is moving, jumping and is not grounded
        {
            walkingAudio.Stop();
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

    //Plays the camera landing animation when the player lands on the ground
    private void PlayerLandingFunction()
    {
        if(isGrounded && !isJumping)
        {
            cameraAnimator.SetBool("IsLanded", true);
        }
        else
        {
            cameraAnimator.SetBool("IsLanded", false);
        }
    }

    private void PlaySwordJumpAnimation()
    {
        if(isJumping && weaponPickAndDrop.isEquiped)
        {
            handAnimator.SetBool("IsJumping", true);
        }
    }

    //Draw a wire sphere around the groundCheck and ceilingCheck to visualize their range
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        Gizmos.DrawWireSphere(ceilingCheck.position, ceilingDistance);
    }
}
