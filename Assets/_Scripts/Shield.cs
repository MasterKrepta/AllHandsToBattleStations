using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Shield : MonoBehaviour
{
    [SerializeField] VisualEffect vfx;



    void OnTriggerEnter(Collider other)
    {
        VisualEffect effect = Instantiate(vfx, other.transform.position, Quaternion.identity);

        effect.SetVector3("SphereCenter", other.transform.position);

        Destroy(effect.gameObject, 1.5f);
    }
    void OnCollisionEnter(Collision other)
    {
        // VisualEffect effect = Instantiate(vfx, transform.position, Quaternion.identity);

        // effect.SetVector3("SphereCenter", other.contacts[0].point);

        // Destroy(effect.gameObject, 1.5f);
    }
}
