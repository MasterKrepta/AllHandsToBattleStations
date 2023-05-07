using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Pool", menuName = "ScriptableObjects/ProjectilePool", order = 1)]
public class SO_Pool : ScriptableObject
{
    [SerializeField]
    public GameObject projectile;

    [SerializeField]
    public int amount;
}
