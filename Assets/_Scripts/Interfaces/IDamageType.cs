using System.Collections;
using System.Collections.Generic;
using AllHandsToBattleStations.Assets._Scripts.Interfaces;
using UnityEngine;

public interface IDamageType
{
    public string DamageName { get; set; }
    public DamageClass DamageClass { get; set; }
    void ApplyDamageType(IDamageType damageType, float damageAmount);
}

