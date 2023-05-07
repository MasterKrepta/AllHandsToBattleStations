using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponAim : MonoBehaviour//, PlayerControls.IMouseActions
{
    //todo get these from a SO
    [SerializeField] private Transform targetReticle;
    [SerializeField] private float rotateSpeed = 30f;

    void Start()
    {
        targetReticle = GameObject.Find("TargetReticle").transform;
    }
    private void Update()
    {
        // Calculate the position of the reticle in the center of the screen
        Vector3 reticleScreenPosition = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);

        // Convert the screen position to a ray
        Ray reticleRay = Camera.main.ScreenPointToRay(reticleScreenPosition);

        // Cast a ray from the camera to the reticle position
        if (Physics.Raycast(reticleRay, out RaycastHit hit))
        {
            // Rotate towards the target reticle
            Quaternion targetRotation = Quaternion.LookRotation(hit.point - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
    }
}
