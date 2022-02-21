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

    private float horizontal = 0f;
    private float vertical = 0f;
    private float bodyHorizontal = 0f;

    private const float RETURN_SPEED = 5f;

    void Update()
    {
        HandleRotationAroundCharacter();
    }

    private void HandleRotationAroundCharacter()
    {
        vertical += Input.GetAxis("Mouse Y") * -1 * mouseSensitivity * 0.1f;

        if (movement.IsMoving && !Input.GetKey(KeyCode.LeftAlt))
        {
            bodyHorizontal += Input.GetAxis("Mouse X") * mouseSensitivity * 0.1f;
            horizontal = MoveBackToDefaultPos(horizontal);
            playerTransform.transform.transform.localEulerAngles = new Vector3(playerTransform.transform.transform.localEulerAngles.x, bodyHorizontal);
            lookTarget.transform.localEulerAngles = new Vector3(Mathf.Clamp(vertical, -maxAngle, maxAngle), horizontal, 0);
        }
        else
        {
            horizontal += Input.GetAxis("Mouse X") * mouseSensitivity * 0.1f;
            lookTarget.transform.localEulerAngles = new Vector3(Mathf.Clamp(vertical, -maxAngle, maxAngle), horizontal, 0);
        }
    }

    private float MoveBackToDefaultPos(float y)
    {
        if (Mathf.Abs(y) > Mathf.Epsilon)
        {
            if (y > 0)
            {
                if (y - RETURN_SPEED < 0)
                {
                    return 0;
                }
                else
                {
                    return y - RETURN_SPEED;
                }
            }
            else
            {
                if (y + RETURN_SPEED > 0)
                {
                    return 0;
                }
                else
                {
                    return y + RETURN_SPEED;
                }
            }
        }
        else
        {
            return 0f;
        }
    }
}
