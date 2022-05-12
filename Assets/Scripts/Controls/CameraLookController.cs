using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookController : MonoBehaviour
{
    [SerializeField] private Transform lookTarget;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerMovementController movement;

    [Header("Look Settings")]
    [SerializeField] float mouseSensitivity = 15f;
    [SerializeField] float maxAngle = 85f;

    private float horizontal = 0f;
    private float vertical = 0f;
    private float bodyHorizontal = 0f;

    private const float RETURN_SPEED = 5f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        HandleRotationAroundCharacter();
    }

    private void HandleRotationAroundCharacter()
    {
        vertical = Mathf.Clamp(vertical + Input.GetAxis("Mouse Y") * -1 * mouseSensitivity * Time.fixedDeltaTime, -maxAngle, maxAngle);
        bodyHorizontal += Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;

        Vector3 prevRot = rb.rotation.eulerAngles;
        rb.rotation = Quaternion.Euler(new Vector3(prevRot.x, bodyHorizontal, prevRot.z));
        lookTarget.transform.localEulerAngles = new Vector3(vertical, 0f, 0f);
        /*
        if (movement.IsMoving && !Input.GetKey(KeyCode.LeftAlt))
        {
            bodyHorizontal += Input.GetAxis("Mouse X") * mouseSensitivity * 0.1f;
            playerTransform.transform.transform.localEulerAngles = new Vector3(playerTransform.transform.transform.localEulerAngles.x, bodyHorizontal);
            horizontal = MoveBackToDefaultPos(horizontal);
            lookTarget.transform.localEulerAngles = new Vector3(Mathf.Clamp(vertical, -maxAngle, maxAngle), horizontal, 0);
        }
        else
        {
            horizontal += Input.GetAxis("Mouse X") * mouseSensitivity * 0.1f;
            lookTarget.transform.localEulerAngles = new Vector3(Mathf.Clamp(vertical, -maxAngle, maxAngle), horizontal, 0);
        }*/
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
