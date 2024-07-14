using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterScene : MonoBehaviour
{
    private bool inEnterArea;
    [SerializeField] private GameObject InteractableE;

    private Transform playerCameraTransform;

    public EnterType enterType;

    public enum EnterType
    {
        ShopEnter,
        PracticeEnter,

        StageEnter
    }
    void Start()
    {
     InteractableE.SetActive(false);
     playerCameraTransform = Camera.main.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inEnterArea = true;
            InteractableE.SetActive(true);
            FacePlayer();
        }
    }

     private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inEnterArea = false;
            InteractableE.SetActive(false);
        }
    }

    void Update()
    {
        if(inEnterArea)
        {
             FacePlayer();
        }
        if(Input.GetKeyDown(KeyCode.E) && inEnterArea)
            {
                if(enterType == EnterType.ShopEnter) SceneManager.LoadScene(2);
                if(enterType == EnterType.StageEnter) SceneManager.LoadScene(1);
                if(enterType == EnterType.PracticeEnter) SceneManager.LoadScene(0);
            }
    }

    private void FacePlayer()
    {
        Vector3 directionToCamera = playerCameraTransform.position - InteractableE.transform.position;
        directionToCamera.y = 0;
        InteractableE.transform.rotation = Quaternion.LookRotation(-directionToCamera);
    }

    
}
