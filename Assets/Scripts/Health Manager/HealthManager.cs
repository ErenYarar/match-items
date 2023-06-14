using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Text HealthCount1_text;
    public Text HealthCount2_text;
    public Text HealthCountTimer_text;
    public int maxHealth = 5;
    public int currentHealth = 5;
    private int minutesPerHealth = 30;
    private float timeSinceLastHealthUpdate = 0f;
    public HealthTimer _timer;

    public static HealthManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // instance oluşturma
        }
        else
        {
            Destroy(gameObject); // instance varsa tekrar oluşturma
        }
    }

    void Start()
    {
        currentHealth = PlayerPrefs.GetInt("CurrentHealth", maxHealth);
        SetHealthText();
        _timer.StartTimer();
    }

    void Update()
    {
        if (currentHealth != PlayerPrefs.GetInt("CurrentHealth", maxHealth) || _timer.timerStarted)
        {
            SetHealthText();
        }
    }

    void SetHealthText()
    {
        HealthCount1_text.text = currentHealth.ToString();
        if (currentHealth < maxHealth)
        {
            _timer.gameObject.SetActive(true);
            HealthCount2_text.gameObject.SetActive(false); //full
            HealthCountTimer_text.gameObject.SetActive(true);
            HealthCountTimer_text.text = _timer.timerText.text;

            if (!_timer.timerStarted)
            {
                _timer.StartTimer();
            }
        }
        else
        {
            _timer.gameObject.SetActive(false);
            HealthCountTimer_text.gameObject.SetActive(false);
            HealthCount2_text.gameObject.SetActive(true);
            HealthCount2_text.text = "FULL";
        }
    }

    public void IncreaseHealth()
    {
        currentHealth++;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        PlayerPrefs.SetInt("CurrentHealth", currentHealth); // currentHealth değerini PlayerPrefs'e kaydediyoruz
        SetHealthText();
    }

    public void DecreaseHealth()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            PlayerPrefs.SetInt("CurrentHealth", currentHealth); // currentHealth değerini PlayerPrefs'e kaydediyoruz
        }
        // SetHealthText();
        // HealthCount1_text ve HealthCount2_text'in null olmadığını kontrol ediyoruz
        if (HealthCount1_text != null && HealthCount2_text != null && HealthCountTimer_text != null)
        {
            SetHealthText();
        }
    }
}
