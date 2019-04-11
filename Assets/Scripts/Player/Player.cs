using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    //movement stuff here
    [Space(5)]
    [Header("Strafe Settings")]
    public float StrafeSpeed =10.0f;
    public float MovementLerpFactor =1.0f;


    //look stuff here
    [Space(5)]
    [Header("Look Settings")]
    [Tooltip("Press Backspace to unlock cursor for debuging")]
    public bool LockCursor= false;
    public bool InvertY = false;
    public bool InvertX = false;
    [Range(.2f, 30.0f)]
    public float XMouseSensitivity = 1.0f;
    [Range(.2f, 30.0f)]
    public float YMouseSensitivity = 1.0f;
    public float minimumX = -360.0f;
    public float maximumX = 360.0f;
    public float minimumY = -60.0f;
    public float maximumY = 60.0f;

    //private look stuff
    private Quaternion originalRotation;
    private float rotationX =0.0f;
    private float rotationY = 0.0f;

    private Rigidbody rb;

    [Space(5)]
    [Header("Gun Settings")]
    public Gun EquipedGun;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        originalRotation = transform.localRotation;

        if (LockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        //STRAFE STUFF HERE

        Vector3 inputVector = GetStrafeInput();
        inputVector.Normalize();
        inputVector.y = 0.0f;
        if (inputVector != Vector3.zero)
        {
            float lerpedSpeed = Mathf.Lerp(0, StrafeSpeed, MovementLerpFactor * Time.deltaTime);
            transform.position += (inputVector  * lerpedSpeed * Time.deltaTime);
        }

        //ROTATION STUFF HERE

        //get mouse controls, adjust for inverted and sestitivity
        rotationX += Input.GetAxis("Mouse X") * (InvertX ? -1 : 1) * XMouseSensitivity;
        rotationY += Input.GetAxis("Mouse Y") * (InvertY ? 1 : -1) * YMouseSensitivity;

        //clamp the angle to stop full rotation
        rotationX = ClampAngle(rotationX, minimumX, maximumX);
        rotationY = ClampAngle(rotationY, minimumY, maximumY);

        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.right);
        
        transform.localRotation = originalRotation * xQuaternion * yQuaternion;
      
        //MISC FUNCTIONS!

        //quit
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        //escape cursor lock
        if (Input.GetKey(KeyCode.Backspace))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

        }

    }

    private Vector3 GetStrafeInput()
    {
        Vector3 movedirection = new Vector3();

  
        if (Input.GetKey(KeyCode.W))
        {
            movedirection += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movedirection -= transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movedirection -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movedirection += transform.right;
        }

        return movedirection;
    }

  

    public float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360.0f) && (angle <= 360.0f))
        {
            if (angle < -360.0f)
            {
                angle += 360.0f;
            }
            if (angle > 360.0f)
            {
                angle -= 360.0f;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}
