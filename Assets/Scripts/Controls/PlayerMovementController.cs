using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform body;

    [Header("Movement Settings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform feetTransform;


    [Header("Stepping Settings")]
    [SerializeField] private GameObject stepRayUpper;
    [SerializeField] private GameObject stepRayLower;
    [SerializeField] private float stepHeight = 0.3f;
    [SerializeField] private float stepSmooth = 0.1f;

    private bool IsOnGround()
    {
        return Physics.CheckSphere(feetTransform.position, 0.15f, groundLayers);
    }

    public bool IsMoving => rb.velocity.magnitude > 0.0f;

    private void Awake()
    {
        stepRayUpper.transform.position = stepRayLower.transform.position + new Vector3(0f, stepHeight, 0f);
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (IsOnGround())
            {
                rb.AddForce(new Vector3(0.0f, jumpForce, 0.0f), ForceMode.Impulse);
            }
        }

        HandleMovement();
        HandleSteps();
    }

    private void HandleSteps()
    {
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitLower, 0.6f, groundLayers))
        {
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitUpper, 0.6f, groundLayers))
            {
                rb.position += new Vector3(0f, stepSmooth * Time.deltaTime, 0f);
            }
        }

        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(1.5f, 0, 1), out RaycastHit hitLower45, 0.6f, groundLayers))
        {
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(1.5f, 0, 1), out RaycastHit hitUpper45, 0.6f))
            {
                rb.position += new Vector3(0f, stepSmooth * Time.deltaTime, 0f);
            }
        }

        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-1.5f, 0, 1), out RaycastHit hitLowerMinus45, 0.6f, groundLayers))
        {

            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(-1.5f, 0, 1), out RaycastHit hitUpperMinus45, 0.6f, groundLayers))
            {
                rb.position += new Vector3(0f, stepSmooth * Time.deltaTime, 0f);
            }
        }
    }

    private void HandleMovement()
    {
        Vector3 sideMovement = body.right * Input.GetAxisRaw("Horizontal");
        Vector3 fwdMovement = body.forward * Input.GetAxisRaw("Vertical");

        var direction = (sideMovement + fwdMovement) * walkSpeed;

        if (direction.magnitude >= 0.1f)
        {
            rb.velocity = new Vector3(direction.x, rb.velocity.y, direction.z);
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
    }
}