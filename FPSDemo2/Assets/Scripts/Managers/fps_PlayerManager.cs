using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    None,
    Idle,
    Walk,
    Crouch,
    Run,
}

public class fps_PlayerManager : MonoBehaviour
{
    private PlayerState state = PlayerState.None;
    public PlayerState State
    {
        get
        {
            if (running)
                state = PlayerState.Run;
            else if (walking)
                state = PlayerState.Walk;
            else if (crouching)
                state = PlayerState.Crouch;
            else
                state = PlayerState.Idle;
            return state;
        }
    }

    public float sprintSpeed = 10.0f;
    public float sprintJumpSpeed = 8.0f;
    public float normalSpeed = 0.1f;
    public float normalJumpSpeed = 1.0f;
    public float walkSpeed = 6.0f;
    public float walkJumpSpeed = 7.0f;
    public float crouchSpeed = 2.0f;
    public float crouchJumpSpeed = 5.0f;
    public float crouchDeltaHeight = 0.5f;   // 蹲伏时下降高度
    public float gravity = 20.0f;
    public float cameraMoveSpeed = 8.0f;

    private float speed;
    private float jumpSpeed;
    private Transform mainCamera;
    private float standardCameraHeight;
    private float crouchCameraHeight;

    private bool grounded = false;
    private bool walking = false;
    private bool crouching = false;
    private bool running = false;

    private Vector3 normalControllerCenter = Vector3.zero;
    private float normalControllerHeight = 0.0f;
    //private float timer = 0;
    private CharacterController controller;
    private fps_PlayerParameter parameter;
    private Vector3 moveDirection = Vector3.zero;
    private Animator animator;

    private void currentSpeed()
    {
        switch(State)
        {
            case PlayerState.Idle:
                speed = normalSpeed;
                jumpSpeed = normalJumpSpeed;
                break;
            case PlayerState.Walk:
                speed = walkSpeed;
                jumpSpeed = walkJumpSpeed;
                break;
            case PlayerState.Crouch:
                speed = crouchSpeed;
                jumpSpeed = crouchJumpSpeed;
                break;
            case PlayerState.Run:
                speed = sprintSpeed;
                jumpSpeed = sprintJumpSpeed;
                break;
        }
        //Debug.Log(speed);
        animator.SetFloat("speed",speed);
    }

    private void updateCrouch()
    {
        if(crouching)
        {
            if(mainCamera.localPosition.y > crouchCameraHeight)
            {
                if (mainCamera.position.y - (crouchDeltaHeight * Time.deltaTime * cameraMoveSpeed) < crouchCameraHeight)
                    mainCamera.localPosition = new Vector3(mainCamera.localPosition.x, crouchCameraHeight, mainCamera.localPosition.z);
                else
                    mainCamera.localPosition -= new Vector3(0, crouchDeltaHeight * Time.deltaTime * cameraMoveSpeed, 0);
            }
            else
            {
                mainCamera.localPosition = new Vector3(mainCamera.localPosition.x, crouchCameraHeight, mainCamera.localPosition.z);
            }
        }
        else
        {
            if (mainCamera.localPosition.y < standardCameraHeight)
            {
                if (mainCamera.position.y + (crouchDeltaHeight * Time.deltaTime * cameraMoveSpeed) > standardCameraHeight)
                    mainCamera.localPosition = new Vector3(mainCamera.localPosition.x, standardCameraHeight, mainCamera.localPosition.z);
                else
                    mainCamera.localPosition += new Vector3(0, crouchDeltaHeight * Time.deltaTime * cameraMoveSpeed, 0);
            }
            else
            {
                mainCamera.localPosition = new Vector3(mainCamera.localPosition.x, standardCameraHeight, mainCamera.localPosition.z);
            }
        }
    }

    private void updateMove()
    {
        if(grounded)
        {
            moveDirection = new Vector3(parameter.inputMoveVector.x, 0, parameter.inputMoveVector.y);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if(parameter.inputJump)
            {
                moveDirection.y = jumpSpeed;
                currentSpeed();
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        CollisionFlags flag =controller.Move(moveDirection * Time.deltaTime);
        grounded = (flag & CollisionFlags.CollidedBelow) != 0;
        
        if(Mathf.Abs(moveDirection.x) > 0 && grounded || Mathf.Abs(moveDirection.z) > 0 && grounded)
        {
            if(parameter.inputSprint)
            {
                walking = false;
                running = true;
                crouching = false;
            }
            else if(parameter.inputCrouch)
            {
                crouching = true;
                running = false;
                walking = false;
            }
            else
            {
                walking = true;
                crouching = false;
                running = false;
            }
        }
        else
        {
            if (walking)
                walking = false;
            if (running)
                running = false;
            if (parameter.inputCrouch)
                crouching = true;
            else
                crouching = false;
        }

        if(crouching)
        {
            controller.height = normalControllerHeight - crouchDeltaHeight;
            controller.center = normalControllerCenter - new Vector3(0, crouchDeltaHeight / 2, 0);
        }
        else
        {
            controller.height = normalControllerHeight;
            controller.height = normalControllerHeight;
        }

        updateCrouch();
        currentSpeed();
        //Debug.Log(speed); 
    }

    // Start is called before the first frame update
    void Start()
    {
        walking = false;
        crouching = false;
        running = false;
        speed = normalSpeed;
        jumpSpeed = normalJumpSpeed;
        mainCamera = GameObject.FindGameObjectWithTag(Tag.mainCamera).transform;
        standardCameraHeight = mainCamera.localPosition.y;
        crouchCameraHeight = standardCameraHeight - crouchDeltaHeight; 
        controller = this.GetComponent<CharacterController>();
        animator = this.GetComponentInChildren<Animator>();
        parameter = this.GetComponent<fps_PlayerParameter>();
        normalControllerCenter = controller.center;
        normalControllerHeight = controller.height;
}

    // Update is called once per frame
    void FixedUpdate()
    {
        updateMove();
    }
}
