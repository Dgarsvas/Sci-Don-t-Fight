using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider col;
    [SerializeField] private CameraLookController lookController;

    public bool IsOnGround
    {
        get;
        private set;
    }

    public bool IsMoving 
    {
        get;
        private set;
    }

    void Update()
    {
        HandleJump();

        if (IsOnGround)
        {
            HandleMovement();
        }
        if (IsMoving)
        {
            HandleRotation();
        }
    }

    private void HandleRotation()
    {
        throw new NotImplementedException();
    }

    private void HandleMovement()
    {
        throw new NotImplementedException();
    }

    private void HandleJump()
    {
        throw new NotImplementedException();
    }
}
