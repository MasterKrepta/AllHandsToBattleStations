using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LeanModel : MonoBehaviour
{
    PlayerControls controls;
    Rigidbody rb;
    Quaternion initialRotation;
    [SerializeField] float leanAmount = 3f;
    [SerializeField] float maxLeanAngle = 25f;
    [SerializeField] float leanTime = 2f;

    [SerializeField] int movementDirection, facingDir, facingRight;

    [SerializeField] float leanX, leanZ;
    [SerializeField] Vector3 forward;
    float dot;
    [SerializeField] Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        initialRotation = transform.localRotation;

    }

    void Update()
    {
        HandleLean();
    }

    private void HandleLean()
    {
        forward = transform.forward;
        facingDir = forward.z >= 0 ? 1 : -1;
        facingRight = forward.x >= 0 ? 1 : -1;
        //TODO we need a fix for when the ship is facing left or right. 
        // if (forward.x > forward.z)
        // {
        //     facingDir = forward.z >= 0 ? 1 : -1;
        // }
        // else
        // {
        //     facingDir = forward.x >= 0 ? 1 : -1;
        // }


        Vector3 velocity = rb.velocity.normalized;

        dot = Vector3.Dot(velocity, forward);
        movementDirection = dot >= 0 ? 1 : -1;

        if (rb.velocity.magnitude > 0.1f)
        {
            // Calculate the rotation angles based on the velocity and ship orientation
            leanX = -rb.velocity.z * movementDirection * leanAmount;
            leanZ = rb.velocity.x * movementDirection * leanAmount;


            // Check if the ship is moving forwards or backwards and adjust the signs of the lean angles accordingly
            if (dot < 0)
            {
                leanX = -leanX;
                leanZ = -leanZ;
            }

            // Clamp the rotation angles to the specified range
            leanX = Mathf.Clamp(leanX, -maxLeanAngle, maxLeanAngle);
            leanZ = Mathf.Clamp(leanZ, -maxLeanAngle, maxLeanAngle);

            //targetRotation = Quaternion.Euler(-leanX * facingDir, rb.transform.localRotation.y, -leanZ * facingDir);
            targetRotation = Quaternion.Euler(-leanX * facingDir, rb.transform.localRotation.y, -leanZ * facingDir);

            // Slerp the rotation from the current rotation to the target rotation
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * leanAmount);
        }
        else
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, initialRotation, leanTime * Time.deltaTime);
            leanX = 0;
            leanZ = 0;
        }
    }
}