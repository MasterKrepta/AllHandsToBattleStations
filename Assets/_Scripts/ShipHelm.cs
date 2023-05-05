using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipHelm : MonoBehaviour, PlayerControls.IPlayerActions
{
    PlayerControls controls;
    [SerializeField] float speed = 5f;
    Vector3 movement;
    Vector3 movementDirection;

    [SerializeField] float leanAmount = 3f;
    [SerializeField] float leanTime = 2f;

    [SerializeField] float rotAmount = 5f;
    float rotY;

    float horizontalInput;
    float verticalInput;
    float t;
    float currentLeanTime = 0f;
    Vector3 thrustDir;
    Quaternion targetRot;
    public ShipHelm(Vector2 thrustValue, Vector2 rotateValue)
    {
        this.thrustValue = thrustValue;
        this.rotateValue = rotateValue;

    }
    public Vector2 thrustValue { get; private set; }
    public Vector2 rotateValue { get; private set; }
    public Vector2 climbValue { get; private set; }
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controls = new PlayerControls();
        controls.Player.Enable();

        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMove;

        controls.Player.Climb.performed += OnClimb;
        controls.Player.Climb.canceled += OnClimb;

        controls.Player.Rotate.performed += OnRotate;
        controls.Player.Rotate.canceled += OnRotate;

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            thrustValue = Vector2.zero;
            return;
        }

        thrustValue = context.ReadValue<Vector2>();
        thrustDir = transform.forward * thrustValue.y * speed;

    }
    public void OnClimb(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            climbValue = Vector2.zero;
            return;
        }

        climbValue = context.ReadValue<Vector2>();
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            rotateValue = Vector2.zero;
            rotY = transform.rotation.eulerAngles.y;
            return;
        }

        rotateValue = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        ApplyRotation();
        ApplyThrust();
        //ApplyClimb();
        //ApplyLean();
    }

    private void ApplyClimb()
    {
        rb.velocity = new Vector3(rb.velocity.x, climbValue.y * speed, rb.velocity.z);
    }

    private void ApplyRotation()
    {
        transform.Rotate(Vector3.up * rotateValue.x * rotAmount * Time.deltaTime);
        rotY = transform.rotation.eulerAngles.y;
    }

    private void ApplyThrust()
    {
        // Vector3 movement = new Vector3(thrustValue.x, climbValue.y, thrustValue.y);
        //rb.velocity = transform.forward * movement * speed;

        horizontalInput = thrustValue.x;
        verticalInput = thrustValue.y;

        movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        movement = movementDirection.normalized * speed;

        rb.velocity = movement;

    }

    private void ApplyLean()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            ResetLeanTime();
        }
        else
        {
            ResetLeanTime();
        }
        Quaternion targetLean = Quaternion.Euler(rb.velocity.z * leanAmount, rotY, -rb.velocity.x * leanAmount);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetLean, Time.fixedDeltaTime * t);
    }

    private void ResetLeanTime()
    {
        currentLeanTime += Time.deltaTime;
        if (currentLeanTime > leanTime)
        {
            currentLeanTime = leanTime;
        }

        t = currentLeanTime / leanTime;
    }


}
