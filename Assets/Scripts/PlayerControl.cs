using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    PlayerControls controls;
    CharacterController controller;
    public Camera camera;
    Vector2 move2;
    public float defaultSpeed = 4f;
    float playerSpeed;
    float upSpeed;
    float gravity;
    
    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = defaultSpeed;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        gravity -= 9.81f;
        upSpeed = gravity;

        Vector3 move3 = Vector3.right * move2.x * playerSpeed + Vector3.forward * move2.y * playerSpeed + Vector3.up * upSpeed;

        controller.Move(move3 * Time.deltaTime);
        if (controller.isGrounded)
        {
            gravity = 0;
        }
    }

    void Jump()
    {

    }

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => move2 = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move2 = Vector2.zero;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
