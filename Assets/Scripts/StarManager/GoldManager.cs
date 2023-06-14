using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public Animator GoldAnim;
    public GameObject GoldObj;
    public Text goldText; // Altın miktarını göstermek için Text elementi
    private float goldValue = 0; // Başlangıçta altın miktarı sıfır
    public static GoldManager instance;

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

    public void AddGold(int value)
    {
        goldValue = PlayerPrefs.GetFloat("Gold"); // PlayerPrefs'den kaydedilen altın miktarını yükle
        goldValue += value; // Altın miktarını arttır
        PlayerPrefs.SetFloat("Gold", goldValue); // PlayerPrefs'e yeni altın miktarını kaydet
        goldText.text = goldValue.ToString(); // Altın miktarını Text elementine yazdır
    }
}
