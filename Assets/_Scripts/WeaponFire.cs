using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponFire : MonoBehaviour, PlayerControls.IWeaponActions
{
    PlayerControls controls;

    //todo get these from a SO
    [SerializeField] GameObject projectile;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] float projectileSpeed = 40f;
    [SerializeField] float fireRate = .25f;

    float nextFireTime = 0f;
    LayerMask FriendlyLayer = 6;
    LayerMask EnemyLayer = 7;

    LayerMask bulletLayer;

    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControls();
        controls.Weapon.Enable();
        controls.Weapon.PrimaryFire.performed += OnPrimaryFire;
        controls.Weapon.SecondaryFire.performed += OnSecondaryFire;

        if (gameObject.layer == FriendlyLayer)
        {
            bulletLayer = EnemyLayer;
        }
        else
        {
            bulletLayer = FriendlyLayer;
        }
    }
    public void OnPrimaryFire(InputAction.CallbackContext context)
    {
        print("Primary Fire Released " + gameObject.name);
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            GameObject newProjectile = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            newProjectile.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
        }
    }

    public void OnSecondaryFire(InputAction.CallbackContext context)
    {
        print("Secondary Fire Released " + gameObject.name);
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            GameObject newProjectile = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            newProjectile.layer = bulletLayer;

            newProjectile.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
        }
    }

}
