using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{

    private PlayerActions actions;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private CharacterController characterController;
    private float HorizontalMouseInput;


    #region INPUT
    private Vector2 moveInput;
    #endregion


    #region VALUE
    [SerializeField]
    private float moveSpeed = 2.0f;
    [SerializeField]
    private float rotationSpeed = 80.0f;
    #endregion

    

    private Vector3 playerVelocity;
    [SerializeField]
    private float gravityValue = -9.81f;

    [SerializeField]
    private float groundedValue = 1.0f;

    [SerializeField]
    private float jumpHeight = 0.2f;
    [SerializeField]
    private float groundedStore = 0;
    [SerializeField]
    private float groundedDecrease = 5.0f;

    public void Gravity()
    {
        playerVelocity.y += gravityValue * 2 * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        if(characterController.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }
    }

    void Awake()
    {
        actions = new PlayerActions();
        actions.Controls.Movement.performed += cxt => moveInput = cxt.ReadValue<Vector2>();
        actions.Controls.Jump.performed += cxt => PerformJump();
        actions.Controls.Mouse.performed += cxt => HorizontalMouseInput = cxt.ReadValue<float>();
    }

    void Start()
    {
        
    }

    private void running()
    {
        animator.SetBool("Running", true);
    }

    private void standing()
    {
        animator.SetBool("Running", false);
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    void Update()
    {
        Move();
        Gravity();
        Rotate();

        groundedStore -= groundedDecrease * Time.deltaTime;

        if (characterController.isGrounded)
        {
            groundedStore = groundedValue;
            animator.SetBool("Jump", false);
            animator.SetBool("Grounded", true);
        }
    }

    private void PerformJump()
    {
        if (groundedStore > 0)
        {
            //Vector3 move = characterController.velocity + transform.up * jumpHeight;
            playerVelocity.y = jumpHeight * -3.0f * gravityValue;
            groundedStore = 0;

            animator.SetBool("Jump", true);
            animator.SetBool("Grounded", false);
        }
    }

    private void Move()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        characterController.Move(moveSpeed * Time.deltaTime * move);

        Debug.Log(moveInput.x);
        Debug.Log(moveInput.y);

        if(moveInput.x == 0 && moveInput.y == 0)
        {
            standing();
        }
        else
        {
            running();
        }
    }

    private void Rotate()
    {
        float mouseX = HorizontalMouseInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }
}
