using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioClip _buttonClip;

    public void PlayGame()
    {
        if (HealthManager.instance.currentHealth > 0)
        {
            SoundManager.Instance.PlaySound(_buttonClip); // Normal Play game sesi
            SceneManager.LoadScene("FoodGame");
            Time.timeScale = 1.0f;
        }
        else
        {
            //SoundManager.Instance.PlaySound(_clip); //Giremez sesi
            Debug.Log("Can 0 Bekle!");
        }
    }

    public void OpenPrivacyWebsite()
    {
        string url = ""; // Privacy politikasýna giden URL eklenmeli
        Application.OpenURL(url);
    }

    public void EmptyAllBtns()
    {
        SoundManager.Instance.PlaySound(_buttonClip);
    }

    public void PauseMenuFunc()
    {
        SoundManager.Instance.PlaySound(_buttonClip);
        Time.timeScale = 0f;
    }

    public void ContinueMenuFunc()
    {
        SoundManager.Instance.PlaySound(_buttonClip);
        Time.timeScale = 1f;
    }

    public void GoToHome()
    {
        SoundManager.Instance.PlaySound(_buttonClip);
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }

    public void GoToHomeFromPause()
    {
        SoundManager.Instance.PlaySound(_buttonClip);
        HealthManager.instance.DecreaseHealth();
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }
}
