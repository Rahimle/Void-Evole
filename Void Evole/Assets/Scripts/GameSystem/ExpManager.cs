using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpManager : MonoBehaviour
{
    // Tao cac bien
    public int currentLevel = 0; // bien level
    public float currentExp = 0f; // bien exp hien tai
    public float expToLevelUp = 10f; // 10 la gia tri can reach de level up
    public float expGrowthMultiplier = 1.2f; // he so tang truong exp, add 20% more exp to level up

    // Bien tham chieu UI
    public Slider expSlider;
    public TMP_Text levelText;
    
    // Start is called before the first frame update
    void Start()
    {
        currentExp = 0f; // start game voi exp = 0

        // Set up value of level at first
        if (expSlider != null)
        {
            expSlider.minValue = 0f;
            expSlider.maxValue = expToLevelUp;
            expSlider.value = currentExp; // set value of slider = 0
        }
        UpdateExpUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Ham gain exp
    public void GainExperience(float amount)
    {
        currentExp += amount; // tang exp

        // vong lap xu ly len nhieu level cung luc
        while(currentExp >= expToLevelUp)
        {
            LevelUp();
        }
        UpdateExpUI();
    }

    // Ham level up
    private void LevelUp()
    {
        currentLevel++; // len level 
        currentExp -= expToLevelUp; // Keep exp thua

        expSlider.maxValue = expToLevelUp; // update max value for slider
        // exp de len level = exp  hien tai * he so tang truong
        expToLevelUp = Mathf.RoundToInt( expToLevelUp * expGrowthMultiplier);
    }

    // Ham cap nhat UI 
    public void UpdateExpUI()
    {
        expSlider.maxValue = expToLevelUp; // set max value
        expSlider.value = currentExp; // cap nhat value 
        levelText.text = "Level " + currentLevel; // hien thi level++
    }
}
