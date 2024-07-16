using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    [SerializeField] public int playerGold;

    [SerializeField] public int playerDamage;

    [SerializeField] public int maxAmmo;

    [SerializeField] public float reloadSpeed;

    [SerializeField] public float recoilCooldown;

    [SerializeField] public float rotateReload;

    public int upgradeDamage;

    public int upgradeAmmo;

    public int upgradeReload;

    public int upgradeCooldown;

    public int upgradeGold;

    public int wave;


    public static Data Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Load();
        }

        else
        {
            Destroy(gameObject);
        }
    }
    
   public void Save()
    {
        PlayerPrefs.SetInt("PlayerGold", playerGold);
        PlayerPrefs.SetInt("PlayerDamage", playerDamage);
        PlayerPrefs.SetInt("MaxAmmo", maxAmmo);
        PlayerPrefs.SetFloat("ReloadSpeed", reloadSpeed);
        PlayerPrefs.SetFloat("RecoilCooldown", recoilCooldown);
        PlayerPrefs.SetFloat("RotateReload", rotateReload);
        PlayerPrefs.SetInt("UpgradeDamage", upgradeDamage);
        PlayerPrefs.SetInt("UpgradeAmmo", upgradeAmmo);
        PlayerPrefs.SetInt("UpgradeReload", upgradeReload);
        PlayerPrefs.SetInt("UpgradeCooldown", upgradeCooldown);
        PlayerPrefs.SetInt("UpgradeGold", upgradeGold);
        PlayerPrefs.SetInt("Wave", wave);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        playerGold = PlayerPrefs.GetInt("PlayerGold", 0);
        playerDamage = PlayerPrefs.GetInt("PlayerDamage", 25);
        maxAmmo = PlayerPrefs.GetInt("MaxAmmo", 6);
        reloadSpeed = PlayerPrefs.GetFloat("ReloadSpeed", 3);
        recoilCooldown = PlayerPrefs.GetFloat("RecoilCooldown", 3);
        rotateReload = PlayerPrefs.GetFloat("RotateReload", -100);
        upgradeDamage = PlayerPrefs.GetInt("UpgradeDamage", 0);
        upgradeAmmo = PlayerPrefs.GetInt("UpgradeAmmo", 0);
        upgradeReload = PlayerPrefs.GetInt("UpgradeReload", 0);
        upgradeCooldown = PlayerPrefs.GetInt("UpgradeCooldown", 0);
        upgradeGold = PlayerPrefs.GetInt("UpgradeGold", 0);
        wave = PlayerPrefs.GetInt("Wave", 0);
    }
}
