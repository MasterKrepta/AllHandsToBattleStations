using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipHelm : MonoBehaviour, PlayerControls.IPlayerActions
{
    PlayerControls controls;
    [SerializeField] float speed = 5f;
    Vector3 thrustDir;
    public Vector2 thrustValue { get; private set; }
    public Vector2 strafeValue { get; private set; }
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
    void Update()
    {
        rb.velocity = new Vector3(thrustValue.x, climbValue.y, thrustValue.y) * speed;



    }


}
