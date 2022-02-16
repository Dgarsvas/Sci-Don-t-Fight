using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookController : MonoBehaviour
{
    [SerializeField] private Transform lookTarget;
    [SerializeField] private PlayerMovementController movement;

    public float mouseSensitivity = 15f;
    
    float x = 0f;
    float y = 0f;

    //float xRotation = 0f;

    void Update()
    {
        HandleRotationAroundCharacter();
        
    }

    private void HandleRotationAroundCharacter() //TODO
    {
        x += Input.GetAxis("Mouse X") * mouseSensitivity * 0.1f;
        y += Input.GetAxis("Mouse Y") * -1 * mouseSensitivity * 0.1f;

        //xRotation -= y;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //lookTarget.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //lookTarget.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        lookTarget.transform.localEulerAngles = new Vector3(y,x,0);
    }
}
