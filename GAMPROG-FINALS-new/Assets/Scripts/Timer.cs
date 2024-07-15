using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour

{
    public float waveTime = 180f;
    [SerializeField] private TextMeshProUGUI Time;

    [SerializeField] private float currentTime;
    [SerializeField] private bool timerStart = true;
    void Start()
    {
      currentTime = waveTime;
      StartCoroutine(Countdown());  
    }
    private IEnumerator Countdown()
    {
        while (currentTime >= 0 && timerStart)
        {
            UpdateTimerDisplay();
            yield return new WaitForSeconds(1f);
            currentTime -= 1f;
        }

        if (currentTime <= 0)
        {
            timerStart = false;
        }
    }

     private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        Time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
