using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider col;
    [SerializeField] private CameraLookController lookController;
    [SerializeField] private float overlapBoxOffset;
    [SerializeField] private Transform lookTarget;

    [Header("Movement Settings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float turnSmoothTime;

    // is this how you declare constants in C# scripts?
    private int numCollidersInPlayerToIgnore;

    private float turnSmoothVelocity;

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
            HandleMovementAndRotation();
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

    private void HandleMovementAndRotation()
    {
        float sideMovement = Input.GetAxisRaw("Horizontal") * walkSpeed;
        float fwdMovement = Input.GetAxisRaw("Vertical") * walkSpeed;

        // If i use rigidbody here then it starts jittering. Probably because physics updates happen every fixed update or something like that.
        // Should I update the position of transform or RB?
        var direction = new Vector3(sideMovement, 0.0f, fwdMovement).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + lookTarget.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(rb.rotation.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            rb.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

            Vector3 moveDir = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
            // Problem with this is that this gives no velocity
            // e.g. when you jump while moving you will jump straight up
            rb.MovePosition(rb.position + moveDir.normalized * walkSpeed * Time.deltaTime);
        }
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
