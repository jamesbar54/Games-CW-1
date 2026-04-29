using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement/ control")]

    #region VALUE
    [SerializeField]
    private float moveSpeed = 2.0f;
    [SerializeField]
    private float rotationSpeed = 80.0f;
    #endregion

    #region INPUT
    private Vector2 moveInput;
    #endregion

    private PlayerActions actions;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private CharacterController characterController;
    private float HorizontalMouseInput;
    [SerializeField]
    private GameObject attack;
    

    [Header("Physics")]
    
    
    [SerializeField]
    private float gravityValue = -9.81f;

    [SerializeField]
    private float groundedValue = 1.0f;
    private Vector3 playerVelocity;

    //Jump values
    [SerializeField]
    private float jumpHeight = 0.2f;
    [SerializeField]
    private float groundedStore = 0;
    [SerializeField]
    private float groundedDecrease = 5.0f;

    //Attack Values
    [SerializeField]
    private float activeState = 0; 
    [SerializeField]
    private float activeTime = 1.3f; 
    private bool attackPerforming = false;


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
        actions.Controls.Attack.performed += cxt => PerformAttack();
        //actions.Menu.MenuKey.performed += cxt => openMenu();
    }

     private void openMenu()
    {
        Debug.Log("Pause");
    }

    void Start()
    {
        //Application.targetFrameRate = 120;
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
        if(Time.timeScale == 1){
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

            if (groundedStore < 0.8f)
            {
                animator.SetBool("Grounded", false);
            }


            if(activeState > 0)
            {
                activeState -= 1 * Time.deltaTime;

                if (activeState < 0.5)
                {
                    animator.SetBool("Attacking", false);
                }
            }
            else if(attackPerforming == true)
            {
                AttackScript attackScript = attack.GetComponent<AttackScript>();
                attackScript.endAttack();

                attackPerforming = false;
            }
        }
    }

    private void PerformJump()
    {
        if (groundedStore > 0 && Time.timeScale == 1)
        {
            //Vector3 move = characterController.velocity + transform.up * jumpHeight;
            playerVelocity.y = jumpHeight * -3.0f * gravityValue;
            groundedStore = 0;

            animator.SetBool("Jump", true);
            animator.SetBool("Grounded", false);
        }
    }

    private void PerformAttack()
    {
        if (activeState <= 0 && animator.GetBool("Grounded") == true && Time.timeScale == 1)
        {
            AttackScript attackScript = attack.GetComponent<AttackScript>();
            attackScript.activateAttack();

            activeState = activeTime;
            
            animator.SetBool("Attacking", true);

            attackPerforming = true;
        }

    }

    private void Move()
    {
        if(activeState <= 0.3f){
            Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
            characterController.Move(moveSpeed * Time.deltaTime * move);


            if(moveInput.x == 0 && moveInput.y == 0)
            {
                standing();
            }
            else
            {
                running();
            }
        }
    }

    private void Rotate()
    {
        float mouseX = HorizontalMouseInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }
}
