using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider col;
    [SerializeField] private Transform body;
    [SerializeField] private float overlapBoxOffset;

    [Header("Movement Settings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float maxSpeed;

    private int numCollidersInPlayerToIgnore;    

    private bool IsOnGround()
    {
        var colliders = Physics.OverlapBox(rb.position + Vector3.down * overlapBoxOffset, transform.localScale / 2);
        return colliders.Length > 1;
    }

    public bool IsMoving => rb.velocity.magnitude > 0.0f;

    private void Start()
    {
        numCollidersInPlayerToIgnore = GetComponentsInChildren<Collider>().Length;
    }

    void FixedUpdate()
    {
        HandleJump();

        if (IsOnGround())
        {
            HandleMovementAndRotation();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(rb.position + Vector3.down * overlapBoxOffset, transform.localScale);
    }

    private void HandleMovementAndRotation()
    {
        Vector3 sideMovement = body.right * Input.GetAxisRaw("Horizontal");
        Vector3 fwdMovement = body.forward * Input.GetAxisRaw("Vertical");

        var direction = (sideMovement + fwdMovement) * walkSpeed;

        if (direction.magnitude >= 0.1f)
        {
            rb.velocity = new Vector3(direction.x, rb.velocity.y, direction.z);
        }
    }

    private void HandleJump() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround())
        {
            rb.AddForce(new Vector3(0.0f, jumpForce, 0.0f), ForceMode.VelocityChange);
            //IsOnGround = false;
        }
    }
}
