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
        rotY = rb.rotation.eulerAngles.y;

        if (rb.velocity.magnitude > 0.1f)
        {
            ResetLeanTime();
        }
        else
        {
            ResetLeanTime();
        }
        //TODO verify if this is the correct way to do this, inverted when we are facing the other way
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