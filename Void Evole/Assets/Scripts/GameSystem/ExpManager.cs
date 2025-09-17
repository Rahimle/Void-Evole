using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpManager : MonoBehaviour
{
    // Tao cac bien
    public int level; // bien level
    public int currentExp; // bien exp hien tai
    public int expToLevel = 10; // 10 la gia tri can reach de level up
    public float expGrowthMultiplier = 1.2f; // he so tang truong exp, add 20% more exp to level up
    public Slider expSlider;
    public TMP_Text currentLevelText;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            GainExperience(2);
        }
    }

    // Ham nhan exp
    public void GainExperience(int amount)
    {
        currentExp += amount; // tang exp
        if(currentExp > expToLevel ) // ktra neu exp reach muc 10
        {
            LevelUp(); // goi ham level up
        }
        UpdateUI();
    }

    // Ham level up
    private void LevelUp()
    {
        level++; // len level 
        currentExp -= expToLevel; // exp hien tai - exp muc = exp con thua
        // exp de len level = exp  hien tai * he so tang truong
        expToLevel = Mathf.RoundToInt( expToLevel * expGrowthMultiplier);
    }

    // Ham cap nhat UI 
    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel; // set max value
        expSlider.value = currentExp; // cap nhat value 
        currentLevelText.text = "Level " + level; // hien thi level++
    }
}
