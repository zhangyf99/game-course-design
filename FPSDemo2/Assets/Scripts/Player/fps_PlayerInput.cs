using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps_PlayerInput : MonoBehaviour
{
    public bool lockCursor
    {
        get { return Cursor.lockState == CursorLockMode.Locked ? true : false; }
        set
        {
            //Cursor.visible = value;
            Cursor.visible = false;
            Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

    private fps_PlayerParameter parameter;
    private fps_Input input;

    private void initialInput()
    {
        parameter.inputMoveVector = new Vector2(input.getAxis("Horizontal"), input.getAxis("Vertical"));
        parameter.inputSmoothLook = new Vector2(input.getAxisRaw("Mouse X"), input.getAxisRaw("Mouse Y"));
        parameter.inputCrouch = input.getButton("Crouch");
        parameter.inputJump = input.getButton("Jump");
        parameter.inputSprint = input.getButton("Sprint");
        parameter.inputFire = input.getButton("Fire");
        //parameter.inputReload = input.getbuttonDown("Reload");
        parameter.inputMap = input.getbuttonDown("Map");
    }

    // Start is called before the first frame update
    void Start()
    {
        lockCursor = true;
        parameter = this.GetComponent<fps_PlayerParameter>();
        input = GameObject.FindGameObjectWithTag(Tag.gameController).GetComponent<fps_Input>();
    }

    // Update is called once per frame
    void Update()
    {
        initialInput();
    }
}
