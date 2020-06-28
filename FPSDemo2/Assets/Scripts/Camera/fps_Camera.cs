using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class fps_Camera : MonoBehaviour
{
    public Vector2 mouseLookSensitivity = new Vector2(4.5f, 4.5f);
    public Vector2 rotationXlimit = new Vector2(87, -87);
    public Vector2 rotationYlimit = new Vector2(-360, 360);
    public Vector3 positionOffset = new Vector3(0, 0, -0.2f);

    private Vector2 currentMouseLook = Vector2.zero;
    private float x_angle = 0;
    private float y_angle = 0;
    private fps_PlayerParameter parameter;
    private Transform m;

    public AnimationCurve RecoilCurve;
    public Vector2 recoliRange;

    private float currentRecoilTime;
    private Vector2 currentRecoil;

    private void getMouseLook()
    {
        currentMouseLook.x = parameter.inputSmoothLook.x;
        currentMouseLook.y = parameter.inputSmoothLook.y;
        currentMouseLook.x *= mouseLookSensitivity.x;
        currentMouseLook.y *= mouseLookSensitivity.y;
        currentMouseLook.y *= -1;
    }

    private void updateInput()
    {
        if (parameter.inputSmoothLook == Vector2.zero)
            return;
        getMouseLook();
        y_angle += currentMouseLook.x;
        x_angle += currentMouseLook.y;

        y_angle = y_angle < -360 ? y_angle + 360 : y_angle;
        y_angle = y_angle > 360 ? y_angle - 360 : y_angle;
        y_angle = Mathf.Clamp(y_angle, rotationYlimit.x, rotationYlimit.y);
        x_angle = x_angle < -360 ? x_angle + 360 : x_angle;
        x_angle = x_angle > 360 ? x_angle - 360 : x_angle;
        x_angle = Mathf.Clamp(x_angle, -rotationXlimit.x, -rotationXlimit.y);

    }


    // Start is called before the first frame update
    void Start()
    {
        parameter = GameObject.FindGameObjectWithTag(Tag.player).GetComponent<fps_PlayerParameter>();
        m = transform;
        m.localPosition = positionOffset;
    }

    // Update is called once per frame
    void Update()
    {
        updateInput();
        CalculatingRecoilOffset();
        Debug.Log(currentRecoil);
    }

    void LateUpdate()
    {
        Quaternion xq = Quaternion.AngleAxis(y_angle, Vector3.up);
        Quaternion yq = Quaternion.AngleAxis(0, Vector3.left);
        m.parent.rotation = xq * yq;

        yq = Quaternion.AngleAxis(-x_angle, Vector3.left);
        m.rotation = xq * yq;

        //CalculatingRecoilOffset();
    }

    void CalculatingRecoilOffset()
    {
        currentRecoilTime += Time.deltaTime;
        float recoilFraction = RecoilCurve.Evaluate(currentRecoilTime);

        currentRecoil = Vector2.Lerp(Vector2.zero, currentRecoil, recoilFraction);
    }

    public void FireTest()
    {
        currentRecoil += recoliRange;
        currentRecoilTime = 0;
        Debug.Log(currentRecoil);
    }

}
