using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Pool", menuName = "ScriptableObjects/PoolItem", order = 1)]
public class SO_Pool : ScriptableObject
{
    [SerializeField]
    public GameObject poolItem;

    [SerializeField]
    public int amount;
}
