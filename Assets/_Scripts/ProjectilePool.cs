using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }
    [SerializeField] List<GameObject> projectilePool;
    [SerializeField] List<SO_Pool> ProjectileType;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (SO_Pool projectileType in ProjectileType)
        {
            PopulatePool(projectileType.projectile, projectileType.amount);
        }

    }

    void PopulatePool(GameObject projectileType, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject newProjectile = Instantiate(projectileType, transform.position, transform.rotation);
            newProjectile.SetActive(false);
            newProjectile.transform.parent = transform;
            projectilePool.Add(newProjectile);
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
        return null;
    }

}
