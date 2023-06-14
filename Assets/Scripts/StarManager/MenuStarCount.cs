using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStarCount : MonoBehaviour
{
    public Text topStarCount;
    public Text chestStarCount;
    public Text chestLevelCount;
    public Text mainLevelCount;
    public static MenuStarCount instance;

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

    private void Start()
    {
        FinalValueStars();
        FinalValueLevels();
    }

    public void FinalValueStars()
    {
        float goldValue = PlayerPrefs.GetFloat("Gold");
        topStarCount.text = goldValue.ToString();

        // 100 olduysa chest çıkar ve tekrar 0/100 yazdır
        if (goldValue >= 100)
        {
            // 100 olduysa chest çıkar
            goldValue -= 100; // Altın miktarından 100 çıkar
            PlayerPrefs.SetFloat("Gold", goldValue); // Altın miktarını kaydet
            chestStarCount.text = "0/100"; // Yeni chest sayısını yazdır
        }
        else
        {
            chestStarCount.text = goldValue.ToString() + "/100"; // Chest sayısını yazdır
        }
    }

    public void FinalValueLevels()
    {
        int levelC = PlayerPrefs.GetInt("LastLevelCompleted", 0);
        int lvlC = levelC + 1;
        mainLevelCount.text = lvlC.ToString();
        // 5 olduysa chest çıkar ve hep artacak şekilde 1/5 - 5/25 - 15/75 şeklinde ilerlet
        chestLevelCount.text = lvlC.ToString() + "/5";
    }
}
