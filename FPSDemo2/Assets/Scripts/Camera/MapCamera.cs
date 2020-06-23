using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    public GameObject fpsPlayer;
    public Transform camTransform;
    // Start is called before the first frame update
    void Start()
    {
        fpsPlayer = GameObject.Find("fpsPlayer");
        camTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(fpsPlayer.transform.localPosition.x, camTransform.localPosition.y, fpsPlayer.transform.localPosition.z);
        this.transform.localPosition = pos;
    }
}
