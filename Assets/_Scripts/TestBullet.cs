using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class TestBullet : MonoBehaviour, PlayerControls.ITESTINGActions
{
    [SerializeField] VisualEffect vfx;
    void Start()
    {
        PlayerControls controls = new PlayerControls();
        controls.TESTING.Enable();
        controls.TESTING.MiddleMouse.performed += OnMiddleMouse;
    }

    public void OnMiddleMouse(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            VisualEffect effect = Instantiate(vfx, hit.point, Quaternion.identity);

            effect.SetVector3("SphereCenter", hit.point);

            Destroy(effect.gameObject, 1.5f);
        }
    }

    // void OnCollisionEnter(Collision other)
    // {
    //     VisualEffect effect = Instantiate(vfx, transform.position, Quaternion.identity);

    //     effect.SetVector3("SphereCenter", other.contacts[0].point);

    //     Destroy(effect.gameObject, 1.5f);
    // }


}
