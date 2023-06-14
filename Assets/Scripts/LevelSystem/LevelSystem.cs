using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem instance;
    public GameObject[] levels;
    public int currentLevel = 0;
    [SerializeField] private Text levelCount_TXT;
    [SerializeField] private Text winpage_levelCount_TXT;

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

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
        //float goldValue = PlayerPrefs.GetFloat("Gold", 0);
    }

    private void OnEnable()
    {
        // SceneManager.sceneLoaded olayına dinleyici ekleniyor
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // SceneManager.sceneLoaded olayından dinleyici kaldırılıyor
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Eğer FoodGame sahnesi yüklendiyse
        if (scene.name == "FoodGame")
        {
            // Level1 GameObjecti açılır
            levels[0].SetActive(true);
            // Diğer tüm leveller kapalı olacak şekilde ayarlanır
            for (int i = 1; i < levels.Length; i++)
            {
                levels[i].SetActive(false);
            }

            // son tamamlanan leveli alın
            int lastLevelCompleted = PlayerPrefs.GetInt("LastLevelCompleted", 0);

            // level objelerini etkinleştirme/kapama
            for (int i = 0; i < levels.Length; i++)
            {
                if (i <= lastLevelCompleted)
                {
                    levels[i].SetActive(true);
                }
                else
                {
                    levels[i].SetActive(false);
                }
            }

            // level sayacını güncelle
            int lvlC = lastLevelCompleted + 1;
            levelCount_TXT.text = lvlC.ToString();
            winpage_levelCount_TXT.text = lvlC.ToString();
            Debug.Log("Current Level: " + (lvlC));
        }
    }


    public void LoadNextLevel()
    {
        if (currentLevel < levels.Length)
        {
            levels[currentLevel].SetActive(false);
            currentLevel++;

            if (currentLevel >= levels.Length)
            {
                Debug.Log("Tebrikler! Tüm seviyeler tamamlandi!");
                return;
            }

            // bir sonraki seviyeyi aç
            levels[currentLevel].SetActive(true);

            // level kaydetme
            PlayerPrefs.SetInt("LastLevelCompleted", currentLevel);

            int lvlC = currentLevel + 1;
            levelCount_TXT.text = lvlC.ToString();
            winpage_levelCount_TXT.text = lvlC.ToString();
            Debug.Log("Current Level: " + (currentLevel + 1));
        }
    }
}
