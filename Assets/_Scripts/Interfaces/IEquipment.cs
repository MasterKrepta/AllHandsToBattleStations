using System;
using System.Collections;

namespace AllHandsToBattleStations.Assets._Scripts.Interfaces
{
    public interface IEquipment
    {
        float powerLevel { get; set; }
        Rating rating { get; set; }
        int installCost { get; set; }
        float powerRequired { get; set; }
        bool isUnlocked { get; set; }
        float cooldownTime { get; set; }

        void Use();
        void Unlock();
        void Upgrade();
        void Enable();
        void Disable();
        IEnumerable coolDown();
        bool GetStatus();

        void TakeDamage(IEquipment equipment, float damageAmount);

    }
}