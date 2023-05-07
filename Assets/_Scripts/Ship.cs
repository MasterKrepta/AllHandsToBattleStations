using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int Team = 1;
    public Transform model;

    HardPoint[] HardPoints;

    // Start is called before the first frame update
    void Start()
    {
        AssignTeam();

        model = transform.GetChild(0);
        HardPoints = model.Find("HardPoints").GetComponentsInChildren<HardPoint>();

    }

    private void AssignTeam()
    {
        //put team 1 on the friendly layer
        if (Team == 1)
        {
            gameObject.layer = 6;
            AssignParentLayerToChildren(gameObject);

        }
        else
        {
            gameObject.layer = 7;
            AssignParentLayerToChildren(gameObject);
        }
    }

    void AssignParentLayerToChildren(GameObject parentObject)
    {
        int parentLayer = parentObject.layer;
        foreach (Transform child in parentObject.transform)
        {
            child.gameObject.layer = parentLayer;
            AssignParentLayerToChildren(child.gameObject);
        }
}
}
