using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class ShootGun : MonoBehaviour
{

    //shooting stuff

    [Header("Gun")]
    public float range;
    public float damage;

    //camera stuff

    [Header("Camera/Shake/Muzzle")]
    public Camera playerCamera;

    private ThirdPersonCamera thirdPersonCamera;
    private CinemachineFreeLook cinemachineFreeLook;

    public Transform Gun;

    public ParticleSystem muzzleFlash;

     //Recoil Cursor
    [Header("Recoil and Cursor")]
    public float recoilSpeed;
    public float recoilReturnSpeed;
    public float cursorRecoilAmount;

    public RectTransform cursorUI;

    private Vector2 originalCursorPos;

    private float cursorTolerance = 10f;

     //private Vector2 originalCursorPos;

    //Reload
    [Header("Ammo/Reload")]
    public int maxAmmoPerClip; 
    public int currentAmmoInClip; 
    public int totalAmmo;
    public float reloadTime;
    private bool isReloading = false;
    public TextMeshProUGUI ammoText;
    public GameObject reloadSymbol;

    /// <summary>
    /// how to make a header
    /// </summary>

    private void Awake()
    {
        thirdPersonCamera = FindObjectOfType<ThirdPersonCamera>();
        cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
        cursorUI = GameObject.Find("Cursor").GetComponent<RectTransform>();
    }

    private void Start()
    {
        currentAmmoInClip = maxAmmoPerClip;
        UpdateAmmoUI();
        reloadSymbol.SetActive(false);
        originalCursorPos = cursorUI.anchoredPosition;
    }

    void Update()
    {
        if (isReloading)
            return;

        Vector3 cameraEuler = playerCamera.transform.eulerAngles;
        Gun.localRotation = Quaternion.Euler(cameraEuler.x, 0, 0);

        if (Input.GetKeyDown(KeyCode.R) && currentAmmoInClip < maxAmmoPerClip)
        {
            StartCoroutine(Reload());
            return;
        }

        if (currentAmmoInClip <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (thirdPersonCamera.currentStyle == ThirdPersonCamera.CameraStyle.Combat && Input.GetKeyDown(KeyCode.Mouse0) && IsCursorOriginal() && !isReloading)
        {
            Shoot();
            muzzleFlash.Play();
            StartCoroutine(CameraShake(0.1f, 0.3f));
            AddRecoil();
            currentAmmoInClip--;
            UpdateAmmoUI();
        }
        FixCursorRecoil();
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    private IEnumerator CameraShake(float duration, float magnitude)
    {
        //Debug.Log("Camera Shake");
        CinemachineBasicMultiChannelPerlin[] perlinChannels = cinemachineFreeLook.GetComponentsInChildren<CinemachineBasicMultiChannelPerlin>();

        foreach (var perlin in perlinChannels)
        {
            perlin.m_AmplitudeGain = magnitude;
            //Debug.Log("Magnitude amplified");
        }

        yield return new WaitForSeconds(duration);

        foreach (var perlin in perlinChannels)
        {
            perlin.m_AmplitudeGain = 0f;
        }
    }

    void AddRecoil()
    {
        Vector2 cursorPos = cursorUI.anchoredPosition;
        cursorPos += new Vector2(Random.Range(-cursorRecoilAmount, cursorRecoilAmount), cursorRecoilAmount);
        cursorUI.anchoredPosition = cursorPos;
    }

    void FixCursorRecoil()
    {
        cursorUI.anchoredPosition = Vector2.Lerp(cursorUI.anchoredPosition, Vector2.zero, recoilReturnSpeed * Time.deltaTime);
    }

     bool IsCursorOriginal()
    {
        return Vector2.Distance(cursorUI.anchoredPosition, originalCursorPos) < cursorTolerance;
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        reloadSymbol.SetActive(true);

        yield return new WaitForSeconds(reloadTime);

        int ammoToReload = maxAmmoPerClip - currentAmmoInClip;
        if (totalAmmo >= ammoToReload)
        {
            currentAmmoInClip += ammoToReload;
            totalAmmo -= ammoToReload;
        }
        else
        {
            currentAmmoInClip += totalAmmo;
            totalAmmo = 0;
        }

        UpdateAmmoUI();
        isReloading = false;
        reloadSymbol.SetActive(false);
    }

    private void UpdateAmmoUI()
    {
        ammoText.text = currentAmmoInClip.ToString() + " / " + totalAmmo.ToString();
    }
}
