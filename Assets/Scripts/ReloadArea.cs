using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadArea : MonoBehaviour
{
    
   [SerializeField] private GameObject reloadText;
   private ShootGun shootGun;

   private int maxTotalAmmo;

   private bool inReloadArea;

   private Transform playerCameraTransform;


   private void Awake()
   {
    shootGun = FindObjectOfType<ShootGun>();
   }

   private void Start()
   {
    maxTotalAmmo = shootGun.totalAmmo;
    reloadText.SetActive(false);
    playerCameraTransform = Camera.main.transform;
   }


   private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inReloadArea = true;
            reloadText.SetActive(true);
            FacePlayer();
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
        if(inReloadArea)
        {
            FacePlayer();
        }
        
        if(Input.GetKeyDown(KeyCode.E) && inReloadArea)
            {
            //Debug.Log("Hello");
            shootGun.totalAmmo = maxTotalAmmo;
            shootGun.UpdateAmmoUI();
            }
    }

    private void FacePlayer()
    {
        Vector3 directionToCamera = playerCameraTransform.position - reloadText.transform.position;
        directionToCamera.y = 0;
        reloadText.transform.rotation = Quaternion.LookRotation(-directionToCamera);
    }
}
