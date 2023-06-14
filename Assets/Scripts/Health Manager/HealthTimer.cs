using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthTimer : MonoBehaviour
{
    public Text timerText;
    public float countdownDuration = 1800f;
    private DateTime startTime;
    public bool timerStarted;

    private void Start()
    {
        if (PlayerPrefs.HasKey("StartTime"))
        {
            // Eğer PlayerPrefs'te kayıtlı bir başlangıç zamanı varsa, zamanlayıcının son durumunu yükleyin
            string dateTimeString = PlayerPrefs.GetString("StartTime");
            startTime = DateTime.Parse(dateTimeString);
            timerStarted = true;
        }
        else
        {
            startTime = DateTime.UtcNow;
            timerStarted = false;
        }
    }

    void Update()
    {
        if (timerStarted)
        {
            TimeSpan timeElapsed = DateTime.UtcNow - startTime;
            float timeLeft = countdownDuration - (float)timeElapsed.TotalSeconds;
            // Debug.Log("Time left: " + timeLeft);
            if (timeLeft > 0)
            {
                UpdateHealthText(timeLeft);
            }
            else
            {
                HealthManager.instance.IncreaseHealth();
                timerStarted = false;
                UpdateHealthText(0f);
            }
        }
    }

    void UpdateHealthText(float timeLeft)
    {
        //Debug.Log("Time left: " + timeLeft);
        if (HealthManager.instance.currentHealth == HealthManager.instance.maxHealth)
        {
            timerText.text = "FULL";
        }
        else
        {
            string formattedTime = FormatTime(timeLeft);
            timerText.text = formattedTime;
        }
    }

    public void StartTimer()
    {
        if (!timerStarted)
        {
            startTime = DateTime.UtcNow;
            timerStarted = true;

            // Başlangıç zamanını PlayerPrefs'e kaydedin
            PlayerPrefs.SetString("StartTime", startTime.ToString());
        }
    }

    public string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
