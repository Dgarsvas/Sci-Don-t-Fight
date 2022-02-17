using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider col;
    [SerializeField] private CameraLookController lookController;
    [SerializeField] private float jumpForce;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float overlapBoxOffset;
    [SerializeField] private Transform lookTarget;

    // is this how you declare constants in C# scripts?
    private int numCollidersInPlayerToIgnore;

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

    private void Start()
    {
        numCollidersInPlayerToIgnore = GetComponentsInChildren<Collider>().Length;
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

    // Debug ground check
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(rb.position + Vector3.down * overlapBoxOffset, transform.localScale);
    }

    private void FixedUpdate()
    {
        // transform is of Player object and doesn't reflect the scale of player mesh. Should we change this to something else?
        var colliders = Physics.OverlapBox(rb.position + Vector3.down * overlapBoxOffset, transform.localScale / 2);
        IsOnGround = colliders.Length > 1;

        IsMoving = rb.velocity.magnitude > 0.0f;
    }

    private void HandleRotation()
    {
       // Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
      // transform.Rotate()
        //rb.MoveRotation(Quaternion.Euler(0.0f, lookTarget.rotation.y, 0.0f));

        // I don't know how to work around this. Can't rotate transforms because then lookTarget also rotates
        // Could move rotation to this script but then CameraLookController.cs doesn't seem useful anymore
        // rotating RB doesn't seem to work for some reason
        // HELP
    }

    private void HandleMovement()
    {
        float sideMovement = Input.GetAxis("Horizontal") * walkSpeed / 2* Time.deltaTime;
        float fwdMovement = Input.GetAxis("Vertical") * walkSpeed * Time.deltaTime;

        // If i use rigidbody here then it starts jittering. Probably because physics updates happen every fixed update or something like that.
        // Should I update the position of transform or RB?
        transform.position += new Vector3(sideMovement, 0.0f, fwdMovement);
    }

    private void HandleJump() 
    {
        if (IsOnGround && Input.GetKeyUp(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0.0f, jumpForce, 0.0f));
            IsOnGround = false;
        }
    }
}
