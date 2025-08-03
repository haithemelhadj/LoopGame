using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float distance = 20f;
    public float scrollSpeed = 10f;
    public float rotationSpeed = 100f;
    public float selfRotationSpeed = 100f;

    public Camera mainCamera;
    public Transform cameraT;
    public Transform targetT;

    public bool invertX = false;
    public int invertXint = 1;
    public bool invertY = false;
    public int invertYint = 1;
    public bool invertZ = false;
    public int invertZint = 1;

    public CorrectPosition correctPositionScript; // Reference to the CorrectPosition script to check if the player is in the correct position

    Vector3 initT;
    Quaternion initR;

    public KeyCode ResetCameraButton;
    public KeyCode ControlCameraButton;

    private void Start()
    {
        

        //invert rotate around x axis
        if (invertX)
            invertXint = -1;
        else
            invertXint = 1;
        //invert rotate around y axis
        if(invertY)
            invertYint = -1;
        else
            invertYint = 1;
        //invert self rotate 
        if(invertZ)
            invertZint = -1;
        else
            invertZint = 1;
        
        //initialize camera position and rotation
        initT = cameraT.position;
        initR = cameraT.rotation;
    }


    private void Update()
    {
        if(!correctPositionScript.isCorrectPosition)
            CameraControl();
        if (Input.GetKeyDown(ResetCameraButton))
        {
            ResetCamera();
        }
    }

    public void CameraControl()
    {
        if(Input.GetKey(ControlCameraButton)) // Left mouse button
        {
            float horizontal = Input.GetAxis("Mouse X") * rotationSpeed *invertXint* Time.deltaTime;
            float vertical = Input.GetAxis("Mouse Y") * rotationSpeed * invertYint * Time.deltaTime;
            
            cameraT.RotateAround(targetT.position, cameraT.up, horizontal);
            cameraT.RotateAround(targetT.position, cameraT.right, vertical);
            
        }
        //rotate camera along z axis
        if(Input.GetAxis("Horizontal") != 0 )
        {
            float horizontal = Input.GetAxis("Horizontal") * selfRotationSpeed * invertZint * Time.deltaTime;
            cameraT.Rotate(new Vector3(0,0,horizontal));
        }


        //zoom in and out
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            distance -= scroll * scrollSpeed;
            distance = Mathf.Clamp(distance, 5f, 80f); // Limit zoom range
            cameraT.position = targetT.position - cameraT.forward * distance;
        }

    }

    public void ResetCamera()
    {
        cameraT.position = initT;
        cameraT.rotation = initR;
    }
}
