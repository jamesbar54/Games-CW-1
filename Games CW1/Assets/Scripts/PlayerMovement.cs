using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
    private PlayerActions actions;
    [SerializeField]
    private CharacterController characterController;
    #region INPUT
    private Vector2 moveInput;
    private bool jump;
    #endregion
    [SerializeField]
    #region VALUE
    private float moveSpeed = 2.0f;
    #endregion
    [SerializeField]
    private float jumpHeight = 0.2f;

    private Vector3 playerVelocity;
    private float gravityValue = -9.81f;

    [SerializeField]
    private float groundedValue = 1.0f;

    [SerializeField]
    private float groundedStore = 0;
    [SerializeField]
    private float groundedDecrease = 5.0f;

    public void Gravity()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
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
    }

    void Start()
    {
        
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

        groundedStore -= groundedDecrease * Time.deltaTime;

        if (characterController.isGrounded)
        {
            groundedStore = groundedValue;
        }
    }

    private void PerformJump()
    {
        if (groundedStore > 0)
        {
            //Vector3 move = characterController.velocity + transform.up * jumpHeight;
            playerVelocity.y = jumpHeight * -3.0f * gravityValue;
            groundedStore = 0;
        }
    }

    private void Move()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        characterController.Move(moveSpeed * Time.deltaTime * move);
    }
}
