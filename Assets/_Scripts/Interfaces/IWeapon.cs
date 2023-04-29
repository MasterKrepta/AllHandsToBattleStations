using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllHandsToBattleStations.Assets._Scripts.Interfaces
{
    public interface IWeapon
    {
        IDamageType damageType { get; set; }
        bool CheckShieldStatus();
        bool CheckArmorStatus();
        bool CheckHullStatus(); // default to hull if no armor
        void FireWeapon();
    }
}