using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public AudioClip digsfx;
    public AudioClip cowsfx;
    public AudioClip jumpsfx;
    public AudioClip walksfx;

    PlayerControls controls;
    CharacterController controller;
    private Animator animator;
    Vector2 move2;
    public float defaultSpeed = 4f;
    public float rotationSpeed;
    public float jumpForce = 6f;
    public float hoverSpeed = -.5f;
    public float upSpeed;
    //public Vector3 orientation;
    bool doHover = false;
    bool hovering = false;
    bool canDig = false;
    float playerSpeed;
    GameObject diggableRef;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        playerSpeed = defaultSpeed;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        //orientation = Vector3.one;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!controller.isGrounded && !hovering)
        {
            upSpeed -= 9.81f*Time.deltaTime; //gravity
        }
        //checks if player can actually hover
        CheckHoverable(); 

        //move2 is a vector2 taken from player input hence y instead of z
        Vector3 move3 = (Camera.main.transform.right * move2.x * playerSpeed ) + (Camera.main.transform.forward * move2.y * playerSpeed ); 
        //movement
        controller.Move(move3 * Time.deltaTime);
        //Jumping
        controller.Move(Vector3.up * upSpeed * Time.deltaTime );
        //rotate the GameObject
        transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y ,0);
        
    }

    void Jump()
    {
        doHover = true; //bool for attempting to hover; doHover automatically set as false when released jump button; 
        if (controller.isGrounded) //can only jump from ground
        {
            upSpeed = jumpForce;
        }
        source.PlayOneShot(jumpsfx);


    }

    void CheckHoverable()
    {
        if (doHover)
        {
            if (!controller.isGrounded && upSpeed <= 0) //is in air and past apex of jump height
            {
                hovering = true;
                upSpeed = hoverSpeed;
                source.Stop();
            }

        }source.Play();
    }
        
    

    void Awake()
    {
        controls = new PlayerControls();
        //to add controls, edit PlayerControls -> add an action to Gameplay Action map -> add desired keybinds. Save asset when done.
        controls.Gameplay.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Gameplay.Move.canceled += ctx => MoveEnd();
        controls.Gameplay.Jump.performed += ctx => Jump(); //hover is set to true in Jump()
        controls.Gameplay.Jump.canceled += ctx => doHover = hovering = false;
        controls.Gameplay.Dig.performed += ctx => Dig();
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

    /*void ChangeOrientation(bool x=false, bool y=false, bool z=false)
    {
        orientation = new Vector3(x?orientation.x*-1:orientation.x,y?orientation.y*-1:orientation.y,z?orientation.z*-1:orientation.z);
    }*/

    void Move(Vector2 input)
    {
        move2 = input;
        animator.SetBool("IsWalking", true);
        source.PlayOneShot(walksfx);

    }

    void MoveEnd()
    {
        move2 = Vector2.zero;
        animator.SetBool("IsWalking", false);
    }

    void Dig() {
        Debug.Log("dig");
        if (canDig) {
            diggableRef.SetActive(false);
        }
        source.PlayOneShot(digsfx);

    }

    void OnTriggerEnter(Collider x) {
        if (x.tag == "Diggable") {
            canDig = true;
            diggableRef = x.gameObject;
            Debug.Log("You can dig");
        }
        if (x.tag == "Milkable")
        {
            canDig = true;
            diggableRef = x.gameObject;
            Debug.Log("You can dig");
            source.PlayOneShot(cowsfx);

        }

    }
    void OnTriggerExit(Collider x) {
        if (x.tag == "Diggable") {
            canDig = false;
            diggableRef = null;
            Debug.Log("You can't dig");
        }
    }
}
