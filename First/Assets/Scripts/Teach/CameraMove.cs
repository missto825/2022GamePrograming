using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    public GameObject TargetObject = null;
    public float Distance = 5.0f;
    public Vector3 Rotate = Vector3.zero;
    public Vector2 MoveVal;
    public float MoveSpeed = 5.0f;

    private bool _is_rotate_ready = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetPos = TargetObject.transform.position;
        Vector3 cameraDir = targetPos - (targetPos + (Vector3.forward * Distance));
        Vector3 cameraPos = Quaternion.Euler(Rotate) * cameraDir;
        transform.position = cameraPos;
        transform.LookAt(targetPos);

        if (MoveVal!= Vector2.zero)
        {
            TargetObject.transform.position += Quaternion.Euler(Rotate) 
                * new Vector3(MoveVal.x, 0.0f, MoveVal.y);
        }   
    }

    //카메라 회전 처리
    public void RotateCamera(InputAction.CallbackContext inContext)
    {

        if (_is_rotate_ready)
        {
            Vector3 inputAxis = inContext.ReadValue<Vector2>();
            float plich = Rotate.x + inputAxis.y;
            Rotate.x += Mathf.Min(80.0f, Mathf.Abs(inputAxis.y) * Mathf.Sign(plich));
            Rotate.y += inputAxis.x;
        }
    }

    public void ReadyCameraRotate(InputAction.CallbackContext inContext)
    {
        Debug.Log("Ready!");
        _is_rotate_ready = inContext.performed;
    }

    public void MoveTarget(InputAction.CallbackContext inContext)
    {
        MoveVal = inContext.ReadValue<Vector2>();
    }
}
