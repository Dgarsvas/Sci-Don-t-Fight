using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookController : MonoBehaviour
{
    [SerializeField] private Transform lookTarget;
    [SerializeField] private PlayerMovementController movement;
    
    void Update()
    {
        if (!movement.IsMoving)
        {
            HandleRotationAroundCharacter();
        }
    }

    private void HandleRotationAroundCharacter()
    {
        throw new NotImplementedException();
    }
}
