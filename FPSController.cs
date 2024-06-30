using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    
    [SerializeField] CharacterController controller;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Camera playerCamera;

    //Input Values
    float mouseX;
    float mouseY;
    float mouseSensitivity = 500f;
    float horzAxis;
    float vertAxis;

    // Player movement values
    float xRotation = 0f;
    float yRotation = 0f;
    float moveSpeed = 5f;
    Vector3 velocity;
    float gravity = -9.81f;
    [SerializeField] float jumpHeight = 1f;
    float groundDistance = 0.3f;
    bool isGrounded;
    bool jumpTrigger;

    public GameObject audioObject;

    public AudioManager am;
    float sinceLastFootstep;
    float timeBetweenFootsteps = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        am = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        CheckGrounded();
        CheckJumpTrigger();
        ApplyMouseDirection();
        ApplyMovement();
    }

    void GetInputs()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        horzAxis = Input.GetAxis("Horizontal");
        vertAxis = Input.GetAxis("Vertical");
        jumpTrigger = Input.GetButtonDown("Jump");
    
    }

    void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded & velocity.y < 0f)
        {
            velocity.y = 0f;
        }
        else if(!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime; 
        }
    }

    void CheckJumpTrigger()
    {
        if(jumpTrigger && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            am.AudioTrigger(AudioManager.SoundFXCat.Jumping, transform.position, 1f);
        }
    }

    void ApplyMouseDirection()
    {
        // able to look up and down based on mouse input
        yRotation -= mouseY * mouseSensitivity * Time.deltaTime;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);

        // able to look up and down based on mouse input
        xRotation = mouseX * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * xRotation);
    }

    void ApplyMovement()
    {
        Vector3 move = (transform.right * horzAxis) + (transform.forward * vertAxis);
        controller.Move(move * moveSpeed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);

        sinceLastFootstep += Time.deltaTime;

        if(sinceLastFootstep > timeBetweenFootsteps && move.magnitude != 0 && isGrounded) 
        {
            sinceLastFootstep = 0f;
            am.AudioTrigger(AudioManager.SoundFXCat.Footsteps, transform.position, 1f);
        }
    }
}
