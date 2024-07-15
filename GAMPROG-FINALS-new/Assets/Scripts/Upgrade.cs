using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private UpgradeType upgradeType;
    public enum UpgradeType
    {
        Damage,
        Ammo,
        Reload,
        Cooldown,

    }

    internal void BuyUpgrade()
    {
        if(upgradeType == UpgradeType.Damage) 
        {
            if(Data.Instance.playerGold >= 500)
            {
                Data.Instance.playerDamage += 25; 
                Data.Instance.playerGold -= 500;
            }
        }
        
        if(upgradeType == UpgradeType.Ammo)
        {
            if(Data.Instance.playerGold >= 500)
            {
                Data.Instance.maxAmmo += 2;
                Data.Instance.playerGold -= 500;
            }
        }

        if(upgradeType == UpgradeType.Reload)
         {
            if(Data.Instance.playerGold >= 500)
            {
                Data.Instance.reloadSpeed -= 0.5f; 
                Data.Instance.playerGold -= 500;
            }
        }

        if(upgradeType == UpgradeType.Cooldown)
         {
            if(Data.Instance.playerGold >= 500)
            {
               Data.Instance.recoilCooldown += 0.5f;
                Data.Instance.playerGold -= 500;
            }
        }
    }
}
