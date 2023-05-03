using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanModel : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float leanAmount = 3f;
    [SerializeField] float maxLeanAngle = 30f;
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
    }
    void FixedUpdate()
    {

        if (rb.velocity.magnitude > 0.1f)
        {
            ResetLeanTime();
        }
        else
        {
            ResetLeanTime();
        }

        // TestLean();
        // return;

        //rotY = rb.rotation.eulerAngles.y;
        rotY = rb.velocity.y;
        //int dir = rotY > 180 ? -1 : 1;

        //TODO verify if this is the correct way to do this, inverted when we are facing the other way
        // Quaternion targetLean = Quaternion.Euler(rb.velocity.z * leanAmount, rotY, rb.velocity.x * leanAmount);
        // transform.rotation = Quaternion.Lerp(transform.rotation, targetLean, Time.fixedDeltaTime * t);

        float x = -rb.velocity.x; // might be negative, just test it
        float z = rb.velocity.z;
        Vector3 euler = transform.localEulerAngles;
        euler.z = Mathf.Lerp(euler.x, x, (leanAmount) * leanTime);
        euler.x = Mathf.Lerp(euler.z, z, (leanAmount) * leanTime);

        transform.localEulerAngles = euler;
        //print($"x: {x} z: {z} euler: {euler}");

    }

    void Update()
    {
        // //Bank
        // float bankX = -rb.velocity.x;
        // //float bankZ = rb.velocity.z;
        // if (bankX != 0.0f)
        // {
        //     rotZ += bankX * bankScale * Time.deltaTime;
        //     rotZ = ClampAngle(rotZ, -bankRange, bankRange);
        // }
        // else
        // {
        //     rotZ = Mathf.MoveTowardsAngle(rotZ, 0.0f, returnSpeed * Time.deltaTime);
        // }
        // transform.localRotation = Quaternion.Euler(0.0f, 0.0f, rotZ);
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