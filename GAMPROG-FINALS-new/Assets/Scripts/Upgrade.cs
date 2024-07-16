using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    private PlayerMovement playerMovement;
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] private Image upgradeImage;

    [SerializeField] private Sprite[] upgradeSprites;

    private int upgradeLevel = 0;
    public enum UpgradeType
    {
        Damage,
        Ammo,
        Reload,
        Cooldown,

    }

    private void Awake()
    {
            playerMovement = FindObjectOfType<PlayerMovement>();
    }

    internal void BuyUpgrade()
    {
        if(upgradeType == UpgradeType.Damage) 
        {
            if(Data.Instance.playerGold >= 500 && Data.Instance.playerDamage < 125)
            {
                Data.Instance.playerDamage += 25; 
                Data.Instance.playerGold -= 500;
                playerMovement.UpdateGoldUI();
                UpgradeLevel();
            }
        }
        
        if(upgradeType == UpgradeType.Ammo)
        {
            if(Data.Instance.playerGold >= 500 && Data.Instance.maxAmmo < 14)
            {
                Data.Instance.maxAmmo += 2;
                Data.Instance.playerGold -= 500;
                playerMovement.UpdateGoldUI();
                UpgradeLevel();
            }
        }

        if(upgradeType == UpgradeType.Reload)
         {
            if(Data.Instance.playerGold >= 500 && Data.Instance.reloadSpeed > 1)
            {
                Data.Instance.reloadSpeed -= 0.5f; 
                Data.Instance.rotateReload += -80;
                Data.Instance.playerGold -= 500;
                playerMovement.UpdateGoldUI();
                UpgradeLevel();
            }
        }

        if(upgradeType == UpgradeType.Cooldown && Data.Instance.recoilCooldown < 5)
         {
            if(Data.Instance.playerGold >= 500)
            {
               Data.Instance.recoilCooldown += 0.5f;
               Data.Instance.playerGold -= 500;
               playerMovement.UpdateGoldUI();
               UpgradeLevel();
            }
        }
    }

    private void UpgradeLevel()
    {
        upgradeLevel++;
        if (upgradeLevel < upgradeSprites.Length)
        {
            upgradeImage.sprite = upgradeSprites[upgradeLevel];
        }
    }
}
