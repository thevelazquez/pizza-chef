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
    private GameObject interactiveRef;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private int sneaks = 0;
    private bool isRunning;
    public bool isAttacking = false;
    private bool isHovering;
    private bool isJumping;
    private bool isGrounded;
    private bool isSneaking;
    private bool canCollect;
    public AudioClip SwingSFX;

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
        if (interactiveRef == null) {
            return;
        }
        if (canCollect) {
            //interactiveRef.SetActive(false);
            interactiveRef.GetComponent<CollectibleController>().PickUp();
            return;
        }
        if (interactiveRef.tag == "Teleporter") {
            interactiveRef.GetComponent<TPScript>().changeScene();
        }
        if (interactiveRef.tag == "NPC")
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }

    void Sneak()
    {
        //add animator cue
        isSneaking = !isSneaking;

        if (isSneaking == true)
        {
            animator.SetBool("IsSneaking", true);
            if (sneakSpeed >= 0)
            {
                animator.SetBool("IsMoving", true);
            }
        }
        
        if (isSneaking == false)
        {
            animator.SetBool("IsSneaking", false);
            if (sneakSpeed <= 0)
            {
                animator.SetBool("IsMoving", false);
            }
        }
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
        isAttacking = true;
        animator.SetLayerWeight(animator.GetLayerIndex("Combat Layer"), 1);
        animator.SetTrigger("Attack");
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(SwingSFX);
        StartCoroutine(SwingCooldown());


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
        switch (x.tag) {
            case "Collectible":
                canCollect = true;
                interactiveRef = x.gameObject;
                break;
            case "Milkable":
                canCollect = true;
                interactiveRef = x.gameObject;
                break;
            case "Teleporter":
                interactiveRef = x.gameObject;
                interactiveRef.GetComponent<DialogueTrigger>().TriggerDialogue();
                break;
            case "Sneak":
                sneaks++;
                break;
            case "Farmer":
                SceneManager.LoadScene("LoseMenu");
                break;
            case "NPC":
                interactiveRef = x.gameObject;
                interactiveRef.GetComponent<DialogueTrigger>().TriggerDialogue();
                break;
            default:
                Debug.Log(x.tag);
                break;
        }/*
        if (x.tag == "Milkable") {
            canCollect = true;
            interactiveRef = x.gameObject;
        }
        if (x.tag == "Teleporter") {
            interactiveRef = x.gameObject;
            interactiveRef.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
        if (x.tag == "Sneak") {
            sneaks++;
        }
        if (x.tag == "Farmer")
        {
            SceneManager.LoadScene("LoseMenu");
        }
        if (x.tag == "NPC")
        {
            interactiveRef = x.gameObject;
            interactiveRef.GetComponent<DialogueTrigger>().TriggerDialogue();
        }*/
    }

    void OnTriggerExit(Collider x) {
        switch (x.tag) {
            case "Sneak":
                sneaks--;
                break;
            case "NPC":
                FindObjectOfType<DialogueManager>().EndDialogue();
                break;
            case "Teleporter":
                FindObjectOfType<DialogueManager>().EndDialogue();
                interactiveRef = null;
                break;
            default:
                canCollect = false;
                interactiveRef = null;
                break;
        }/*
        if (x.tag == "Milkable") {
            canCollect = false;
            interactiveRef = null;
        }
        if (x.tag == "Teleporter") {
            interactiveRef = null;
        }
        if (x.tag == "Sneak") {
            sneaks--;
        }
        if (x.tag == "NPC")
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }*/
    }
    IEnumerator SwingCooldown()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        isAttacking = false;

    }
}