using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleLevelBar : MonoBehaviour
{
    public Image _bar;

    private void Update()
    {
        // LevelChange(LevelSystem.instance.currentLevel);
        LevelChange();
    }

    void LevelChange()
    {
        int levelC = PlayerPrefs.GetInt("LastLevelCompleted", 0);
        int lvlC = levelC + 1;

        float amount = (lvlC/100.0f) * 180.0f/360;
        _bar.fillAmount = amount;
        // displaylevel_txt.text = LevelSystem.instance.currentLevel.ToString();
        MenuStarCount.instance.mainLevelCount.text = lvlC.ToString();
    }
}
