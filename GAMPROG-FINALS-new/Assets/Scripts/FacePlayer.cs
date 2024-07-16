using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    [SerializeField] private GameObject UI;

    private Transform playerCameraTransform;

    private void Start()
    {
     playerCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        Vector3 directionToCamera = playerCameraTransform.position - UI.transform.position;
        directionToCamera.y = 0;
        UI.transform.rotation = Quaternion.LookRotation(-directionToCamera);
    }

}
