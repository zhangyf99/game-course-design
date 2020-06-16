using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //the position of player
    public Transform target;
    //smooth the movement of camera 
    public float smoothing = 5f;

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // the orgin vector from player to camera
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        // the new vector from player to camera except offset which means the new position of the camera
        Vector3 targetCamPos = target.position + offset;
        //lerp for smooth move
        transform.position = Vector3.Lerp(transform.position, targetCamPos,smoothing*Time.fixedDeltaTime);
    }
}
