using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaffaController : MonoBehaviour
{
    [SerializeField]
    public float maximumSpeed;

    [SerializeField]
    public float rotationSpeed;

    [SerializeField]
    public float jumpSpeed;

    [SerializeField]
    public float sneakSpeed;

    [SerializeField]
    public float hoverSpeed;

    [SerializeField]
    public float jumpButtonGracePeriod;

    [SerializeField]
    private Transform cameraTransform;

    private Animator animator;
    private CharacterController characterController;
    private PlayerControls controls;
    private Vector2 move;
    private GameObject diggableRef;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private int sneaks = 0;
    private bool isRunning;
    private bool isHovering;
    private bool isJumping;
    private bool isGrounded;
    private bool isSneaking;
    private bool canDig;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    void Awake()
    {
        controls = new PlayerControls();
        //to add controls, edit PlayerControls -> add an action to Gameplay Action map -> add desired keybinds. Save asset when done.
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
        controls.Gameplay.Sprint.performed += ctx => isRunning = true;
        controls.Gameplay.Sprint.canceled += ctx => isRunning = false;
        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.Jump.canceled += ctx => isHovering = false;
        controls.Gameplay.Interact.performed += ctx => Interact();
        controls.Gameplay.Attack.performed += ctx => Attack();
        controls.Gameplay.Block.performed += ctx => Block();
        controls.Gameplay.Sneak.performed += ctx => Sneak();
    }

    void Interact()
    {
        if (canDig) {
            diggableRef.SetActive(false);
        }
    }

    void Sneak()
    {
        //add animator cue
        isSneaking = !isSneaking;
    }

    public bool IsSneaking()
    {
        return (sneaks>0)?isSneaking:false;
    }

    void Jump()
    {
        jumpButtonPressedTime = Time.time;
        isHovering = true;
    }

    //Use this to enable player input
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    //Use this to disable player input
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movementDirection = new Vector3(move.x, 0, move.y);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        if (isRunning)
        {
            inputMagnitude *= 2;
        }

        animator.SetFloat("Speed", inputMagnitude, 0.05f, Time.deltaTime);

        float speed = inputMagnitude * maximumSpeed;
        if (isSneaking)
        {
            speed = sneakSpeed;
        }

        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();
        
        if(isHovering && ySpeed < 0)
        {
            ySpeed = -hoverSpeed;
        } else 
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
            animator.SetBool("IsJumping", false);
            isJumping = false;
            animator.SetBool("IsFalling", false);

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                animator.SetBool("IsJumping", true);
                isJumping = true;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }

        else
        {
            characterController.stepOffset = 0;
            animator.SetBool("IsGrounded", false);
            isGrounded = false;

            if ((isJumping && ySpeed < 0) || ySpeed < -1)
            {
                animator.SetBool("IsFalling", true);
            }
        }

        Vector3 velocity = movementDirection * speed;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        else
        {
            animator.SetBool("IsMoving", false);
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void Attack()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Combat Layer"), 1);
        animator.SetTrigger("Attack");
    }

    void Block()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Combat Layer"), 1);
        animator.SetTrigger("Defend");
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void OnTriggerEnter(Collider x) {
        if (x.tag == "Diggable") {
            canDig = true;
            diggableRef = x.gameObject;
            Debug.Log("You can dig");
        }
        if (x.tag == "Sneak") {
            sneaks++;
        }
        if (x.tag == "Farmer")
        {
            SceneManager.LoadScene("LoseMenu");
        }
    }

    void OnTriggerExit(Collider x) {
        if (x.tag == "Diggable") {
            canDig = false;
            diggableRef = null;
            Debug.Log("You can't dig");
        }
        if (x.tag == "Sneak") {
            sneaks--;
        }
    }
}