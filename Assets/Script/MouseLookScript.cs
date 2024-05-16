using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookScript : MonoBehaviour
{
    [HideInInspector] Transform myCamera;
    [HideInInspector] public float timer;
    [HideInInspector] public int int_timer;
    [HideInInspector] public float zRotation;
    [HideInInspector] public float wantedZ;
    [HideInInspector] public float timeSpeed = 2;
    public float mouseSensitvity = 0;
    public float mouseSensitvity_notAiming = 2;
    [HideInInspector]
    public float mouseSensitvity_aiming = 50;
    [HideInInspector] public float timerToRotatez;
    private float rotationYVelocity, cameraXVelocity;
    public float yRotationSpeed, xCameraSpeed;
    [HideInInspector]
    public float wantedYRotation;
    [HideInInspector]
    public float currentYRotation;
    [HideInInspector]
    public float wantedCameraXRotation;
    [HideInInspector]
    public float currentCameraXRotation;
    public float topAngleView = 15;
    public float bottomAngleView = -11;
    public float RightAngleView = 50;
    public float leftAngleView = -60;
    
    void Awake() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        myCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Start() 
    {
        
    }

    void Update()
    {
        MouseInputMovement();
        if (Input.GetKeyDown(KeyCode.L))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void HeadMovement()
    {
        timer += timeSpeed * Time.deltaTime;
        int_timer = Mathf.RoundToInt(timer);
        if (int_timer % 2 == 0)
        {
            wantedZ = -1;
        }
        else
        {
            wantedZ = 1;
        }
        zRotation = Mathf.Lerp(zRotation, wantedZ, Time.deltaTime * timerToRotatez);
    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Fire2") != 0)
        {
            mouseSensitvity = mouseSensitvity_aiming;
        }
        else
        {
            mouseSensitvity = mouseSensitvity_notAiming;
        }
        ApplyingStuff();
    }

    void MouseInputMovement()
    {
        wantedYRotation += Input.GetAxis("Mouse X") * mouseSensitvity;
        wantedCameraXRotation -= Input.GetAxis("Mouse Y") * mouseSensitvity;
        wantedCameraXRotation = Mathf.Clamp(wantedCameraXRotation, bottomAngleView, topAngleView);
        wantedYRotation = Mathf.Clamp (wantedYRotation, leftAngleView, RightAngleView);
    }

    void ApplyingStuff()
    {
        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, yRotationSpeed);
        currentCameraXRotation = Mathf.SmoothDamp(currentCameraXRotation, wantedCameraXRotation, ref cameraXVelocity, xCameraSpeed);
        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        myCamera.localRotation = Quaternion. Euler (currentCameraXRotation, 0, zRotation);
    }
}
