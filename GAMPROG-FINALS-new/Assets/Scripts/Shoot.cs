using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class ShootGun : MonoBehaviour
{

    //shooting stuff

    [Header("Gun")]
    [SerializeField] private float range;
    [SerializeField] private int damage;

    //camera stuff

    [Header("Camera/Shake/Muzzle")]
    [SerializeField] private Camera playerCamera;

    private ThirdPersonCamera thirdPersonCamera;
    private CinemachineFreeLook cinemachineFreeLook;

    [SerializeField] private Transform Gun;

    [SerializeField] private ParticleSystem muzzleFlash;

     //Recoil Cursor
    [Header("Recoil and Cursor")]
    [SerializeField] private float recoilSpeed;
    [SerializeField] private float recoilReturnSpeed;
    [SerializeField] private float cursorRecoilAmount;

    [SerializeField] private RectTransform cursorUI;

    [SerializeField] private GameObject cursorActiveUI;

    [SerializeField] private GameObject cursorOutlineUI;

    [SerializeField] private float rotationSpeed;

    private Vector2 originalCursorPos;

    [SerializeField] private float cursorTolerance;

    //Reload
    [Header("Ammo/Reload")]
    [SerializeField] private int maxAmmoPerClip; 
    [SerializeField] private int currentAmmoInClip; 
    public int totalAmmo;
    [SerializeField] private float reloadTime;
    public bool isReloading = false;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private GameObject reloadSymbol;

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

        //saving data for upgrades
        damage = Data.Instance.playerDamage;
        maxAmmoPerClip = Data.Instance.maxAmmo;
        reloadTime = Data.Instance.reloadSpeed;
        recoilReturnSpeed = Data.Instance.recoilCooldown;
        rotationSpeed = Data.Instance.rotateReload;
    }

    private void Update()
    {
        if (isReloading)
            return;

        Vector3 cameraEuler = playerCamera.transform.eulerAngles;
        Gun.localRotation = Quaternion.Euler(cameraEuler.x, 0, 0);

        if (Input.GetKeyDown(KeyCode.R) && currentAmmoInClip < maxAmmoPerClip && totalAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (currentAmmoInClip <= 0 && totalAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (thirdPersonCamera.currentStyle == ThirdPersonCamera.CameraStyle.Combat && Input.GetKeyDown(KeyCode.Mouse0) && IsCursorOriginal() && !isReloading && currentAmmoInClip > 0)
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

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null) 
            {
                target.TakeDamage(damage);
            }

            EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
            if (enemy != null) 
            {
                enemy.TakeDamage(damage);
            }

            Upgrade upgrade = hit.transform.GetComponent<Upgrade>();
            if(upgrade != null)
            {
                upgrade.BuyUpgrade();
                Data.Instance.Save();
                damage = Data.Instance.playerDamage;
                maxAmmoPerClip = Data.Instance.maxAmmo;
                reloadTime = Data.Instance.reloadSpeed;
                recoilReturnSpeed = Data.Instance.recoilCooldown;
                rotationSpeed = Data.Instance.rotateReload;
            }

            Timer timer = hit.transform.GetComponent<Timer>();
            if(timer != null)
            {
                //Debug.Log("hit timer");
                timer.StartCountdown();
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

    private void AddRecoil()
    {
        Vector2 cursorPos = cursorUI.anchoredPosition;
        cursorPos += new Vector2(Random.Range(-cursorRecoilAmount, cursorRecoilAmount), cursorRecoilAmount);
        cursorUI.anchoredPosition = cursorPos;
    }

    private void FixCursorRecoil()
    {
        cursorUI.anchoredPosition = Vector2.Lerp(cursorUI.anchoredPosition, Vector2.zero, recoilReturnSpeed * Time.deltaTime);
    }

    private bool IsCursorOriginal()
    {
        return Vector2.Distance(cursorUI.anchoredPosition, originalCursorPos) < cursorTolerance;
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        cursorActiveUI.SetActive(false);
        cursorOutlineUI.SetActive(false);
        reloadSymbol.SetActive(true);
        StartCoroutine(Rotate());

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
        cursorActiveUI.SetActive(true);
        cursorOutlineUI.SetActive(true);
        reloadSymbol.SetActive(false);
    }

    internal void UpdateAmmoUI()
    {
        ammoText.text = currentAmmoInClip.ToString() + " / " + totalAmmo.ToString();
    }

    private IEnumerator Rotate()
    {
        while (isReloading)
        {
        reloadSymbol.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        yield return null;
        }
    }

}
