using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    [SerializeField] AudioSource[] soundSource;
    private bool mutedSound = false;

    public static SoundManager Instance;

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

    private void Start()
    {
        if (!PlayerPrefs.HasKey("mutedSound"))
        {
            PlayerPrefs.SetInt("mutedSound", 0);
            Load();
        }
        else
        {
            Load();
        }

        UpdateSoundButtonIcon();
    }


    public void OnSoundButtonPress()
    {
        if (mutedSound == false)
        {
            mutedSound = true;
            foreach (AudioSource source in soundSource)
            {
                source.mute = true;
            }
        }
        else
        {
            mutedSound = false;
            foreach (AudioSource source in soundSource)
            {
                source.mute = false;
            }
        }
        UpdateSoundButtonIcon();
        Save();
    }

    public void PlaySound(AudioClip clip)
    {
        if (!mutedSound)
        {
            foreach (AudioSource source in soundSource)
            {
                source.PlayOneShot(clip);
            }
        }
    }

    private void UpdateSoundButtonIcon()
    {
        if (mutedSound == false)
        {
            soundOnIcon.enabled = true;
            soundOffIcon.enabled = false;
        }
        else
        {
            soundOnIcon.enabled = false;
            soundOffIcon.enabled = true;
        }
    }
    
    private void Load()
    {
        if (PlayerPrefs.HasKey("mutedSound"))
        {
            mutedSound = PlayerPrefs.GetInt("mutedSound") == 1;
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt("mutedSound", mutedSound ? 1 : 0);
    }

}
