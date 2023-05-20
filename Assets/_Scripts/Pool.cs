using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Pool : MonoBehaviour
{
    public static Pool Instance { get; private set; }
    [SerializeField] List<GameObject> projectilePool;
    [SerializeField] List<SO_Pool> ProjectileType;

    [SerializeField] List<GameObject> vfxPool;
    [SerializeField] List<SO_Pool> vfxType;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (SO_Pool projectileType in ProjectileType)
        {
            PopulatePool(projectileType.poolItem, projectilePool, projectileType.amount);
        }

        foreach (SO_Pool vfxType in vfxType)
        {
            PopulatePool(vfxType.poolItem, vfxPool, vfxType.amount);
        }

    }

    void PopulatePool(GameObject poolType, List<GameObject> pool, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject newPoolItem = Instantiate(poolType, transform.position, transform.rotation);
            newPoolItem.SetActive(false);
            newPoolItem.transform.parent = transform;
            pool.Add(newPoolItem);
        }
    }

    public GameObject GetProjectile(GameObject projectileType)
    {
        foreach (GameObject projectile in projectilePool)
        {
            if (projectile.name != projectileType.name + "(Clone)") continue;

            if (!projectile.activeInHierarchy)
            {
                projectile.SetActive(true);
                return projectile;
            }
        }
        //Automataically grow pool
        GameObject newProjectile = Instantiate(projectileType, transform.position, transform.rotation);
        Debug.Log($"Growing Pool: new projectile {newProjectile.name}");
        return newProjectile;
    }

    public GameObject GetVFX(VisualEffect vfxType)
    {
        foreach (GameObject vfx in vfxPool)
        {
            if (vfx.name != vfxType.name + "(Clone)") continue;

            if (!vfx.activeInHierarchy)
            {
                vfx.SetActive(true);
                return vfx;
            }
        }
        //Automataically grow pool
        GameObject newVFX = Instantiate(vfxType.gameObject, transform.position, transform.rotation);
        Debug.Log($"Growing Pool: new vfx {newVFX.name}");
        return newVFX;
    }

}
