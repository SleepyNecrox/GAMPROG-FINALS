using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadArea : MonoBehaviour
{
   
   private ShootGun shootGun;

   private int maxTotalAmmo;

   bool inReloadArea;

   public GameObject reloadText;

   private void Awake()
   {
    shootGun = FindObjectOfType<ShootGun>();
   }

   private void Start()
   {
    maxTotalAmmo = shootGun.totalAmmo;
    reloadText.SetActive(false);
   }


   private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inReloadArea = true;
            reloadText.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inReloadArea = false;
            reloadText.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && inReloadArea)
            {
            //Debug.Log("Hello");
            shootGun.totalAmmo = maxTotalAmmo;
            shootGun.UpdateAmmoUI();
            }
    }
}
