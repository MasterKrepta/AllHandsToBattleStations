using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanGPT : MonoBehaviour
{
    public float targetPitch = 0f;  // Target pitch angle in degrees
    public float targetRoll = 0f;   // Target roll angle in degrees
    public float maxAngularSpeed = 30f;  // Maximum angular speed in degrees per second

    private Rigidbody rb;  // Reference to the rigidbody component
    private Quaternion initialRotation;  // Initial rotation of the gameobject
    private Quaternion parentRotation;  // Rotation of the parent gameobject

    private void Start()
    {
        // Get the reference to the rigidbody component
        rb = GetComponentInParent<Rigidbody>();

        // Save the initial rotation of the gameobject
        initialRotation = transform.rotation;

        // Get the rotation of the parent gameobject
        parentRotation = transform.parent.rotation;
    }

    private void FixedUpdate()
    {
        // Calculate the current velocity magnitude of the rigidbody
        float velocityMagnitude = rb.velocity.magnitude;

        // If the velocity magnitude is zero or very small, reset the rotation to level
        if (velocityMagnitude < 0.1f)
        {
            transform.rotation = initialRotation;
            return;
        }

        // Get the rotation of the parent gameobject
        parentRotation = transform.parent.rotation;

        // Calculate the target pitch and roll angles based on the direction the parent gameobject is facing
        Vector3 forward = parentRotation * Vector3.forward;
        Vector3 up = parentRotation * Vector3.up;
        Vector3 right = Vector3.Cross(forward, up);
        float pitchAngle = Mathf.Rad2Deg * Mathf.Atan2(Vector3.Dot(right, Vector3.up), Vector3.Dot(forward, Vector3.up));
        float rollAngle = Mathf.Rad2Deg * Mathf.Atan2(Vector3.Dot(up, Vector3.forward), Vector3.Dot(up, Vector3.up));
        float targetPitchAngle = targetPitch * Mathf.Sign(pitchAngle);
        float targetRollAngle = targetRoll * Mathf.Sign(rollAngle);

        // Calculate the pitch error and roll error, i.e. the difference between the current angle and the target angle
        float pitchError = Mathf.DeltaAngle(transform.eulerAngles.x, targetPitchAngle);
        float rollError = Mathf.DeltaAngle(transform.eulerAngles.z, targetRollAngle);

        // Calculate the pitch speed and roll speed, i.e. the angular speed needed to reach the target angle
        float pitchSpeed = Mathf.Clamp(pitchError, -maxAngularSpeed, maxAngularSpeed);
        float rollSpeed = Mathf.Clamp(rollError, -maxAngularSpeed, maxAngularSpeed);

        // Calculate the new rotation based on the pitch and roll speeds
        Quaternion newRotation = Quaternion.Euler(transform.eulerAngles.x + pitchSpeed * Time.fixedDeltaTime, 0f, transform.eulerAngles.z + rollSpeed * Time.fixedDeltaTime);

        // Apply the new rotation to the gameobject
        transform.rotation = newRotation;
    }
}


