using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationsManager : MonoBehaviour
{
    public bool OnValueChanged = false;
    [SerializeField] GameObject VibOnButton;
    [SerializeField] GameObject VibOffButton;

    public static VibrationsManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            //First run, set the instance
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (Instance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("vibrationArea"))
        {
            PlayerPrefs.SetInt("vibrationArea", 0);
            //Load();
        }
        else
        {
            Load();
        }
    }

    public void OnVibrationsButtonPress()
    {
        OnValueChanged = false;
        VibOnButton.SetActive(false);
        VibOffButton.SetActive(true);
        Save();
    }

    public void OffVibrationsButtonPress()
    {
        OnValueChanged = true;
        VibOnButton.SetActive(true);
        VibOffButton.SetActive(false);
        Save();
    }

    void Load()
    {
        OnValueChanged = PlayerPrefs.GetInt("vibrationArea") == 1;
        if (OnValueChanged)
        {
            VibOnButton.SetActive(true);
            VibOffButton.SetActive(false);
        }
        else
        {
            VibOnButton.SetActive(false);
            VibOffButton.SetActive(true);
        }
    }

    void Save()
    {
        PlayerPrefs.SetInt("vibrationArea", OnValueChanged ? 1 : 0);
    }
}
