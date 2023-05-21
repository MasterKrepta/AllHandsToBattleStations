using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Shield : MonoBehaviour
{
    [SerializeField] VisualEffect vfx;
    float lifeTime = 1.1f;
    Vector3 ShieldOffset;

    [SerializeField] Vector3 ShieldSize = new Vector3(2, 1.5f, 2);
    void Awake()
    {
        ShieldOffset = GetComponent<Collider>().bounds.size + ShieldSize;
    }

    void OnTriggerEnter(Collider other)
    {

        //get color from material of bullet
        Color color = other.GetComponentInChildren<Renderer>().material.color;

        VisualEffect effect = Pool.Instance.GetVFX(vfx).GetComponent<VisualEffect>();

        ConfigureEffect(color, effect);

        effect.SetVector3("SphereCenter", other.transform.position);
        other.gameObject.SetActive(false);

        StartCoroutine(DisableAfterTime(effect.gameObject));
    }

    private void ConfigureEffect(Color color, VisualEffect effect)
    {
        effect.transform.position = transform.position;
        effect.transform.rotation = Quaternion.identity;
        effect.transform.localScale = ShieldOffset;
        effect.SetGradient("HitColor", new Gradient()
        {
            colorKeys = new GradientColorKey[]
            {
                new GradientColorKey(color, 0),
                new GradientColorKey(color, 1)
            }
        });

    }
    //disable newProjectile after lifetime

    IEnumerator DisableAfterTime(GameObject newEffect)
    {
        yield return new WaitForSeconds(lifeTime);
        newEffect.SetActive(false);
    }

}
