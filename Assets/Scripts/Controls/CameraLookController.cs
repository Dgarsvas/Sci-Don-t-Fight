using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookController : MonoBehaviour
{
    [SerializeField] private Transform lookTarget;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerMovementController movement;

    [Header("Look Settings")]
    [SerializeField] float mouseSensitivity = 15f;
    [SerializeField] float maxAngle = 85f;

    private float x = 0f;
    private float y = 0f;

    void Update()
    {
        HandleRotationAroundCharacter();

    }

    private void HandleRotationAroundCharacter() //TODO
    {
        x += Input.GetAxis("Mouse X") * mouseSensitivity * 0.1f;
        y += Input.GetAxis("Mouse Y") * -1 * mouseSensitivity * 0.1f;

        lookTarget.transform.localEulerAngles = new Vector3(Mathf.Clamp(y, -maxAngle, maxAngle), x, 0);
        lookTarget.transform.position = playerTransform.position;
    }
}
