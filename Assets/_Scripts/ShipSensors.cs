using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipSensors : MonoBehaviour, PlayerControls.ISensorsActions
{
    //TODO get this from a Scriptable Object
    [SerializeField] float scannerRange = 100f;
    public Ship Target;

    public LayerMask CurrentTargetLayer;
    LayerMask FriendlyLayer = 6;
    LayerMask EnemyLayer = 7;
    [SerializeField] Collider closestTarget = null;
    Collider[] hitColliders;
    PlayerControls controls;

    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControls();
        controls.Sensors.Enable();
        controls.Sensors.SelectTarget.performed += OnSelectTarget;
        //CurrentTargetLayer = EnemyLayer;
    }

    // Update is called once per frame
    void Update()
    {
        //todo - Toggle between friendly/enemy mode

        //scan for targets on the currenttargetlayer
        hitColliders = Physics.OverlapSphere(transform.position, scannerRange, CurrentTargetLayer);

    }

    private void SetClosestTarget(Collider[] hitColliders)
    {
        //pick the closest target
        float closestDistance = Mathf.Infinity;

        foreach (Collider hit in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = hit;
            }
        }
        Target = closestTarget.GetComponent<Ship>();
    }

    private void ChangeTarget()
    {


        //TODO - get the next target in the hitcolliders array or loop back to the first one
        if (hitColliders.Length > 0)
        {
            if (closestTarget == null)
            {
                SetClosestTarget(hitColliders);
            }
            else
            {
                int index = System.Array.IndexOf(hitColliders, closestTarget);
                if (index < hitColliders.Length - 1)
                {
                    index++;
                    //print(index + " has been incremented");
                }
                else
                {
                    index = 0;
                    //print(index + " has been reset");
                }
                print(index);
                closestTarget = hitColliders[index];
                Target = closestTarget.GetComponent<Ship>();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, scannerRange);
    }

    public void OnSelectTarget(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            ChangeTarget();
        }
    }
}
