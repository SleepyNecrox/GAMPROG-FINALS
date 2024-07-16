using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrade : MonoBehaviour
{
    private PlayerMovement playerMovement;
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] private TextMeshProUGUI upgradeText;

    private int upgradeLevel;
    public enum UpgradeType
    {
        Damage,
        Ammo,
        Reload,
        Cooldown,

        Gold

    }

    private void Awake()
    {
            playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
         if(upgradeType == UpgradeType.Damage) 
        {
            upgradeLevel = Data.Instance.upgradeDamage;
            upgradeText.text = upgradeLevel.ToString();
        }

         if(upgradeType == UpgradeType.Ammo) 
        {
            upgradeLevel = Data.Instance.upgradeAmmo;
            upgradeText.text = upgradeLevel.ToString();
        }

         if(upgradeType == UpgradeType.Reload) 
        {
            upgradeLevel = Data.Instance.upgradeReload;
            upgradeText.text = upgradeLevel.ToString();
        }

         if(upgradeType == UpgradeType.Cooldown) 
        {
            upgradeLevel = Data.Instance.upgradeCooldown;
            upgradeText.text = upgradeLevel.ToString();
        }

         if(upgradeType == UpgradeType.Gold) 
        {
            upgradeLevel = Data.Instance.upgradeGold;
            upgradeText.text = upgradeLevel.ToString();
        }
    }

    internal void BuyUpgrade()
    {
        if(upgradeType == UpgradeType.Damage) 
        {
            if(Data.Instance.playerGold >= 500 && Data.Instance.playerDamage < 125)
            {
                Data.Instance.upgradeDamage++;
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
                Data.Instance.upgradeAmmo++;
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
                Data.Instance.upgradeReload++;
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
               Data.Instance.upgradeCooldown++;
               Data.Instance.recoilCooldown += 0.5f;
               Data.Instance.playerGold -= 500;
               playerMovement.UpdateGoldUI();
               UpgradeLevel();
            }
        }

        if(upgradeType == UpgradeType.Gold)
         {
               Data.Instance.upgradeGold++;
               Data.Instance.playerGold += 500;
               playerMovement.UpdateGoldUI();
               UpgradeLevel();
        }
    }

    private void UpgradeLevel()
    {
        upgradeLevel++;
        upgradeText.text = upgradeLevel.ToString();
    }
}
