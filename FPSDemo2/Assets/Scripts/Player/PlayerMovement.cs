using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRb;
    int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        //get the floor layer
        floorMask = LayerMask.GetMask("Floor");

        anim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
        Animating(h,v);
    }

    void Move(float h,float v)
    {
        movement.Set(h, 0f, v);
        movement=movement.normalized*speed*Time.fixedDeltaTime;

        //move by the movement vector
        playerRb.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        //a ray from the main camera to the mouse
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;


        //Debug.Log(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask));
        //function:Physics.Raycast(direction&start of the ray,the information of collision,ray/s length,collider)
        if (Physics.Raycast(camRay,out floorHit,camRayLength,floorMask))
        {
            //get the rotation vector and freeze the y direction
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            //rotate by the rotation vector
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            //Debug.Log(newRotation);
            playerRb.MoveRotation(newRotation);
            //Debug.Log(playerRb.transform.rotation);

        }

    }

    void Animating(float h,float v)
    {
        //animate by the walking variable
        bool walking = ((h != 0f) || (v != 0f));
        anim.SetBool("IsWalking", walking);
    }

}
