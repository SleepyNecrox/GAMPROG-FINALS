using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPractice : MonoBehaviour
{
    private bool inEnterPractice;
    [SerializeField] private GameObject InteractablePracticeE;

    private Transform playerCameraTransform;
    void Start()
    {
     InteractablePracticeE.SetActive(false);
     playerCameraTransform = Camera.main.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inEnterPractice = true;
            InteractablePracticeE.SetActive(true);
            FacePlayer();
        }
    }

     private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inEnterPractice = false;
            InteractablePracticeE.SetActive(false);
        }
    }

    void Update()
    {
        if(inEnterPractice)
        {
             FacePlayer();
        }
        if(Input.GetKeyDown(KeyCode.E) && inEnterPractice)
            {
            SceneManager.LoadScene(0);
            }
    }

    private void FacePlayer()
    {
        Vector3 directionToCamera = playerCameraTransform.position - InteractablePracticeE.transform.position;
        directionToCamera.y = 0;
        InteractablePracticeE.transform.rotation = Quaternion.LookRotation(-directionToCamera);
    }

    
}
