using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanModel : MonoBehaviour
{
    Rigidbody rb;
    Quaternion initialRotation, targetRotation;
    [SerializeField] float leanAmount = 3f;
    [SerializeField] float maxLeanAngle = 25f;
    [SerializeField] float leanTime = 2f;
    float rotY;
    float t;
    float currentLeanTime = 0f;

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
        if (rb.velocity.magnitude > 0.1f)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);

            float dot = Vector3.Dot(rb.velocity.normalized, forward);
            int direction = dot >= 0 ? 1 : -1;

            // Calculate the rotation angles based on the velocity and ship orientation
            float leanX = -rb.velocity.z * direction * leanAmount;
            float leanZ = rb.velocity.x * direction * leanAmount;

            // Check if the ship is moving forwards or backwards and adjust the signs of the lean angles accordingly
            if (dot < 0)
            {
                leanX = -leanX;
                leanZ = -leanZ;
            }

            // Clamp the rotation angles to the specified range
            leanX = Mathf.Clamp(leanX, -maxLeanAngle, maxLeanAngle);
            leanZ = Mathf.Clamp(leanZ, -maxLeanAngle, maxLeanAngle);

            targetRotation = Quaternion.Euler(-leanX, 0, -leanZ);
            //TODO fix issue where rotation is not applied to initial rotation. 

            // Slerp the rotation from the current rotation to the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * leanAmount);
        }
        else
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, initialRotation, leanTime * Time.deltaTime);
        }
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