using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    private PlayerMovement playerMovement;

    private ShootGun shootGun;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerObj;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform combatLookAt;

    [SerializeField] private GameObject thirdPersonCam;
    [SerializeField] private GameObject combatCam;

    [SerializeField] private GameObject crossHair;

    [SerializeField] private GameObject crossHairOuter;

    public CameraStyle currentStyle;
    public enum CameraStyle
    {
        Basic,
        Combat
    }

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        shootGun = FindObjectOfType<ShootGun>();
    }


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        thirdPersonCam.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1)) SwitchCameraStyle(CameraStyle.Basic);
        if (Input.GetKeyDown(KeyCode.Mouse1)) SwitchCameraStyle(CameraStyle.Combat);

        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        if(currentStyle == CameraStyle.Basic)
        {
        playerMovement.moveSpeed = 7f;
        crossHair.SetActive(false);
        crossHairOuter.SetActive(false);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(inputDir != Vector3.zero)
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }

        else if(currentStyle == CameraStyle.Combat)
        {
            if(!shootGun.isReloading)
            {
            crossHair.SetActive(true);
            crossHairOuter.SetActive(true);
            }
            playerMovement.moveSpeed = 4f;
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            playerObj.forward = dirToCombatLookAt.normalized;
        }
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        combatCam.SetActive(false);
        thirdPersonCam.SetActive(false);

        if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.Combat) combatCam.SetActive(true);

        currentStyle = newStyle;
    }
}