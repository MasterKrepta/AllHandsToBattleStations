using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanModel : MonoBehaviour
{
    Rigidbody rb;
    Quaternion initialRotation;
    [SerializeField] float leanAmount = 3f;
    [SerializeField] float maxLeanAngle = 25f;
    [SerializeField] float leanTime = 2f;
    float rotY;
    float t;
    float currentLeanTime = 0f;


    //Bank
    float maxBank = 90.0f; //Degrees/second
    float bankScale = 60.0f; //Degrees/unitInput*second
    float returnSpeed = 40.0f;//Degrees/second
    float bankRange = 20.0f; //Degrees
    float rotZ = 0.0f; //Degrees

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
        // Calculate the direction the ship is moving in
        Vector3 forward = rb.transform.TransformDirection(rb.transform.forward);

        float dot = Vector3.Dot(rb.velocity.normalized, forward);
        int direction = dot >= 0 ? 1 : -1;

        if (rb.velocity.magnitude > 0.1f)
        {
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

            // Apply the rotation to the child transform
            transform.Rotate(leanX * -leanAmount * Time.deltaTime, 0, leanZ * -leanAmount * Time.deltaTime);
        }
        else
    {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, initialRotation, leanTime * Time.deltaTime);

        }
}

    private void TestLean()
    {
        // Calculate target lean quaternion based on rigidbody velocity
        Quaternion targetLean = Quaternion.Euler(rb.velocity.z * leanAmount, rb.velocity.y, -rb.velocity.x * leanAmount);

        // Calculate the maximum allowed angle between current rotation and target lean rotation
        float maxAngle = maxLeanAngle * Time.fixedDeltaTime;

        // Limit rotation angle between current rotation and target lean rotation
        Quaternion limitedTargetLean = Quaternion.RotateTowards(transform.rotation, targetLean, maxAngle);

        // Smoothly interpolate between current rotation and limited target lean rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, limitedTargetLean, leanTime * t);
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

    static float ClampAngle(float angle, float min, float max)
    {
        while (angle < -360.0) angle += 360.0f;
        while (angle > 360.0) angle -= 360.0f;
        return Mathf.Clamp(angle, min, max);
    }
}