using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace AllHandsToBattleStations.Assets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ShipData", menuName = "DATA/ShipData", order = 1)]
    public class ShipData : ScriptableObject
    {
        public string ShipName;
        public int ShipClass;

        public int ShipSpeed;
        public int ShipManeuverability;
        public int ShipArmor;
        public int ShipShields;
        public int ShipHull;

    }
}