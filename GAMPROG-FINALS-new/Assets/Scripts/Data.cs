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
    }
}
