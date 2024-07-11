using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class ShootGun : MonoBehaviour
{
    public float range;
    public float damage;
    public Camera playerCamera;

    public float cooldownDuration = 1f; 
    private float lastShotTime = -Mathf.Infinity;

    private ThirdPersonCamera thirdPersonCamera;
    private CinemachineFreeLook cinemachineFreeLook;
    

    public float recoilAmount = 2f;
    public float recoilSpeed = 10f;
    public float recoilReturnSpeed = 5f;
    private Vector3 recoilOffset;
    public RectTransform cursorUI;
    public float cursorRecoilAmount = 20f;

    public int maxAmmoPerClip = 6; 
    public int currentAmmoInClip; 
    public int totalAmmo = 36;
    public float reloadTime = 2f;
    private bool isReloading = false;
    public TextMeshProUGUI ammoText;
    public GameObject reloadSymbol;

    public Transform Gun;
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

        if (thirdPersonCamera.currentStyle == ThirdPersonCamera.CameraStyle.Combat && Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= lastShotTime + cooldownDuration && !isReloading)
        {
            Shoot();
            StartCoroutine(CameraShake(0.15f, 0.4f));
            AddRecoil();
            currentAmmoInClip--;
            UpdateAmmoUI();
            lastShotTime = Time.time;
        }
        FixRecoil();
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
        CinemachineBasicMultiChannelPerlin[] perlinChannels = cinemachineFreeLook.GetComponentsInChildren<CinemachineBasicMultiChannelPerlin>();

        foreach (var perlin in perlinChannels)
        {
            perlin.m_AmplitudeGain = magnitude;
        }

        yield return new WaitForSeconds(duration);

        foreach (var perlin in perlinChannels)
        {
            perlin.m_AmplitudeGain = 0f;
        }
    }

    void AddRecoil()
    {
        recoilOffset += Vector3.up * recoilAmount;
        Vector2 cursorPos = cursorUI.anchoredPosition;
        cursorPos += new Vector2(Random.Range(-cursorRecoilAmount, cursorRecoilAmount), cursorRecoilAmount);
        cursorUI.anchoredPosition = cursorPos;
    }

    void FixRecoil()
    {
        recoilOffset = Vector3.Lerp(recoilOffset, Vector3.zero, recoilReturnSpeed * Time.deltaTime);
        playerCamera.transform.localEulerAngles -= recoilOffset * recoilSpeed * Time.deltaTime;
    }

    void FixCursorRecoil()
    {
        cursorUI.anchoredPosition = Vector2.Lerp(cursorUI.anchoredPosition, Vector2.zero, recoilReturnSpeed * Time.deltaTime);
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
