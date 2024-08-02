using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour

{
    [SerializeField] private float waveTime;
    [SerializeField] private TextMeshProUGUI Time;

    [SerializeField] internal float currentTime;
    [SerializeField] internal bool timerStart = false;

    [SerializeField] private GameObject timerObject;

    [SerializeField] private GameObject shopArea;

    [SerializeField] private GameObject practiceArea;
    [SerializeField] internal int waveNumber;

    private EnemySpawner enemyspawner;

    private void Awake()
    {
        enemyspawner = FindObjectOfType<EnemySpawner>();
        MeshRenderer meshRenderer = timerObject.GetComponent<MeshRenderer>();
        Collider collider = timerObject.GetComponent<Collider>();
    }

    private void Start()
    {
        currentTime = waveTime;
        waveNumber = Data.Instance.wave;

        if(waveNumber >= 4)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }

    internal void StartCountdown()
    {
        //Debug.Log("Started");
        shopArea.SetActive(false);
        practiceArea.SetActive(false);
        currentTime = waveTime; 
        waveNumber++;
        Data.Instance.wave++;
        //Debug.Log(waveNumber);
        timerStart = true;
        StartCoroutine(Countdown());
        enemyspawner.StartWave();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
    private IEnumerator Countdown()
    {
        //Debug.Log("Started Countdown");
        while (currentTime >= 0 && timerStart)
        {
            UpdateTimerDisplay();
            yield return new WaitForSeconds(1f);
            currentTime -= 1f;
        }

        if (currentTime <= 0)
        {
            timerStart = false;
            shopArea.SetActive(true);
            practiceArea.SetActive(true);
        }

        if(currentTime <= 0 && waveNumber <= 3)
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
    }

     private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        Time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
