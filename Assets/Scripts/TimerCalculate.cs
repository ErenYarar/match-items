using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimerCalculate : MonoBehaviour//, IPointerDownHandler
{
    public float totalTime;
    private float timeLeft;
    public Text timerText;
    public GameObject losePanel;
    public GameObject pauseBTN;
    public GameObject bottomExtraGold_panel;
    public GameObject redBG;
    public GameObject BG_black;

    private bool hasStarted = false;

    void Start()
    {
        timeLeft = totalTime;
        UpdateTimerText();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        UpdateTimerText();

        if (!hasStarted)
        {
            hasStarted = true;
            StartCoroutine(WaitRedBG());
        }

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            losePanel.SetActive(true);
            pauseBTN.SetActive(true); // Oyun yeniden başlatılırsa geri getir..
            BG_black.SetActive(true);
            bottomExtraGold_panel.SetActive(true);
            redBG.SetActive(false);
            string textTimerValue = "00:00";
            timerText.text = textTimerValue;
            Time.timeScale = 0.0f;
        }
    }

    IEnumerator WaitRedBG()
    {
        while (timeLeft > 0 && timeLeft <= 10)
        {
            redBG.SetActive(true);
            yield return new WaitForSeconds(1f);
            redBG.SetActive(false);
            yield return new WaitForSeconds(1f);
        }
        hasStarted = false;
    }

    // public void OnPointerDown(PointerEventData eventData)
    // {
    //     losePanel.SetActive(false);
    // }

    // Süreyi güncelleyen fonksiyon
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = string.Format("{00:00}:{01:00}", minutes, seconds);
    }
}

