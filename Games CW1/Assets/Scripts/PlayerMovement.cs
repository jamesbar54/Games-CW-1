using System.Collections.Generic;
using System.Data;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement/ control")]

    #region VALUE
    [SerializeField]
    private float moveSpeed = 2.0f;
    [SerializeField]
    private float rotationSpeed = 100.0f;
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
    private float mouseSenativity;  

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

    public bool defending = false;

    private float defendRotate = 30f;
    private RaycastHit hit;

    public float rayCastX = 1.56f;
    public float rayCastY = 1f;
    public float rayCastZ = 1.27f;

    [SerializeField]
    private float damage = 10.0f;

    private bool attacking = false;




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
        if(SceneManager.GetActiveScene().name == "Stage 1")
        {
            Debug.Log("stage 1");
            PlayerPrefs.SetFloat("damage", 10);
        }

        actions = new PlayerActions();
        actions.Controls.Movement.performed += cxt => moveInput = cxt.ReadValue<Vector2>();
        actions.Controls.Jump.performed += cxt => PerformJump();
        actions.Controls.Mouse.performed += cxt => HorizontalMouseInput = cxt.ReadValue<float>();
        actions.Controls.Attack.performed += cxt => PerformAttack();
        actions.Controls.Defend.started += cxt => StartDefending();
        actions.Controls.Defend.canceled += cxt => StopDefending();
        
        upadateMouse();

        damage = PlayerPrefs.GetFloat("damage");
    }

    void Start()
    {
        //Application.targetFrameRate = 120;
    }


    void Update()
    {
        if(Time.timeScale == 1){
            if (defending)
            {
                Defend();
            }else
            {
                Move();
                Rotate();
            }

            Gravity();

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
            }
            else if(attackPerforming == true)
            {
                attackPerforming = false;
            }

            if(activeState < 0.4)
            {
                animator.SetBool("Attacking", false);
            }
        }


        if (attacking)
        {
            Debug.DrawRay(transform.position + transform.rotation * new Vector3(rayCastX, rayCastY, rayCastZ + 1f), transform.TransformDirection(Vector3.left), Color.red, 5);
            Debug.DrawRay(transform.position + transform.rotation * new Vector3(rayCastX, rayCastY, rayCastZ), transform.TransformDirection(Vector3.left), Color.red, 5);
            Debug.DrawRay(transform.position + transform.rotation * new Vector3(rayCastX, rayCastY, rayCastZ + 0.5f), transform.TransformDirection(Vector3.left), Color.red, 5);
        
            if(Physics.Raycast( transform.position + transform.rotation * new Vector3(rayCastX, rayCastY, rayCastZ), transform.TransformDirection(Vector3.left), out hit, 4) || Physics.Raycast( transform.position + transform.rotation * new Vector3(rayCastX, rayCastY, rayCastZ + 0.5f), transform.TransformDirection(Vector3.left), out hit, 4) || Physics.Raycast( transform.position + transform.rotation * new Vector3(rayCastX, rayCastY + 0.3f, rayCastZ + 1f), transform.TransformDirection(Vector3.left), out hit, 4))
            {
                HealthScript health = hit.collider.GetComponent<HealthScript>();

                if(health != null)
                {
                    health.takeDamage(damage);
                }

                rockScript rock = hit.collider.GetComponent<rockScript>();

                if (rock)
                {
                    rock.rebound();
                }
            }   
        }
    }

    private void PerformJump()
    {
        if (groundedStore > 0 && Time.timeScale == 1)
        {
            defending = false;

            //Vector3 move = characterController.velocity + transform.up * jumpHeight;
            playerVelocity.y = jumpHeight * -3.0f * gravityValue;
            groundedStore = 0;

            animator.SetBool("Jump", true);
            animator.SetBool("Grounded", false);
        }
    }

    private void PerformAttack()
    {
        // Debug.Log(activeState);
        // Debug.Log(animator.GetBool("Grounded"));


        if (activeState <= 0 && animator.GetBool("Grounded") == true && Time.timeScale == 1)
        {
            activeState = activeTime;
                
            animator.SetBool("Attacking", true);

            // attackPerforming = true;

            // //attack = Instantiate(attackPrefab, transform);
            
            StartCoroutine(delay(0.4f));
            
        }
    }

    System.Collections.IEnumerator delay(float time)
    {
        yield return new WaitForSeconds(time);

        attacking = true;

        yield return new WaitForSeconds(0.5f);

        attacking = false;
    }

    private void StopDefending()
    {
        defending = false;

        transform.Rotate(Vector3.up * -defendRotate);

        animator.SetBool("Blocking", false);
    }

    private void StartDefending()
    {
        transform.Rotate(Vector3.up * defendRotate);

        animator.SetBool("Running", false);

        StartCoroutine(defendDelay(0.2f));
    }

    System.Collections.IEnumerator defendDelay(float time)
    {
        yield return new WaitForSeconds(time);

        defending = true;
    }

    private void Defend()
    {
        if(Time.timeScale == 1)
        {
            if (!(actions.Controls.Jump.inProgress) && groundedStore > 0.8f)
            {
                animator.SetBool("Blocking", true);

                gameObject.GetComponent<PlayerHealth>().IFrames(0.1f);
            }
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

    public void upadateMouse()
    {
        mouseSenativity = PlayerPrefs.GetFloat("mouseSen");
    }

    private void Rotate()
    {
        float mouseX = HorizontalMouseInput * rotationSpeed * mouseSenativity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
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
}
