using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Shield : MonoBehaviour
{
    [SerializeField] VisualEffect vfx;
    Vector3 ShieldOffset;

    [SerializeField] Vector3 ShieldSize;
    void Awake()
    {
        ShieldOffset = GetComponent<Collider>().bounds.size + ShieldSize;
    }

    void OnTriggerEnter(Collider other)
    {
        VisualEffect effect = Instantiate(vfx, transform.position, Quaternion.identity);

        effect.transform.localScale = ShieldOffset;

        effect.SetVector3("SphereCenter", other.transform.position);

        Destroy(effect.gameObject, 1.5f);
    }

}
