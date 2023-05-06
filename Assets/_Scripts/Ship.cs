using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int Team { get; set; }
    public Transform model;

    HardPoint[] HardPoints;

    // Start is called before the first frame update
    void Start()
    {
        model = transform.GetChild(0);
        HardPoints = model.Find("HardPoints").GetComponentsInChildren<HardPoint>();

    }
}
