using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterShop : MonoBehaviour
{
    private bool inEnterShop;
    [SerializeField] private GameObject InteractableShopE;

    private Transform playerCameraTransform;
    void Start()
    {
     InteractableShopE.SetActive(false);
     playerCameraTransform = Camera.main.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inEnterShop = true;
            InteractableShopE.SetActive(true);
            FacePlayer();
        }
    }

     private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inEnterShop = false;
            InteractableShopE.SetActive(false);
        }
    }

    void Update()
    {
        if(inEnterShop)
        {
             FacePlayer();
        }
        if(Input.GetKeyDown(KeyCode.E) && inEnterShop)
            {
            SceneManager.LoadScene(2);
            }
    }

    private void FacePlayer()
    {
        Vector3 directionToCamera = playerCameraTransform.position - InteractableShopE.transform.position;
        directionToCamera.y = 0;
        InteractableShopE.transform.rotation = Quaternion.LookRotation(-directionToCamera);
    }

    
}
