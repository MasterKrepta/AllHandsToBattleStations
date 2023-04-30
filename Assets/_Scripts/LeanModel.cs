using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanModel : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float leanAmount = 3f;
    [SerializeField] float leanTime = 2f;
    float rotY;
    float t;
    float currentLeanTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }
    void FixedUpdate()
    {
        // TestLean();
        // return;

        rotY = rb.rotation.eulerAngles.y;

        if (rb.velocity.magnitude > 0.1f)
        {
            ResetLeanTime();
        }
        else
        {
            ResetLeanTime();
        }

        // //TODO verify if this is the correct way to do this, inverted when we are facing the other way
        // Quaternion targetLean = Quaternion.Euler(rb.velocity.z * leanAmount, rotY, -rb.velocity.x * leanAmount);

        // transform.rotation = Quaternion.Lerp(transform.rotation, targetLean, Time.fixedDeltaTime * t);

        //transform.eulerAngles.y = Mathf.Clamp(transform.eulerAngles.y, -leanAmount, leanAmount);

        float x = -rb.velocity.x; // might be negative, just test it
        float z = rb.velocity.z;
        Vector3 euler = transform.localEulerAngles;
        euler.z = Mathf.Lerp(euler.x, x, leanAmount * leanTime);
        euler.x = Mathf.Lerp(euler.z, z, leanAmount * leanTime);
        transform.localEulerAngles = euler;
    }

    private void TestLean()
    {
        //print(rb.velocity);

        if (rb.velocity.magnitude > 0.1f)
        {
            // get the rotation towards the velocity direction
            Quaternion lookRotation = Quaternion.LookRotation(rb.velocity, transform.up);

            // apply a lean rotation on top of that
            Quaternion leanRotation = Quaternion.Euler(0f, 0f, -rb.velocity.x / leanAmount);

            // combine the two rotations
            Quaternion targetRotation = lookRotation * leanRotation;

            // apply the rotation using a smooth interpolation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
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