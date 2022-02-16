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
        HandleRotationAroundCharacter();
    }

    private void HandleRotationAroundCharacter() //TODO
    {
        throw new NotImplementedException();
    }
}
