using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpManager : MonoBehaviour
{
    // Singleton Exp
    public static ExpManager Instance { get; private set; }

    // Exp Status
    public int level; 
    public int currentExp; 
    public int expToLevel = 10; 
    public float expGrowthMultiplier = 1.2f;

    // Tham chieu UI
    public Slider expSlider;
    public TMP_Text currentLevelText;

    // Awake Singleton
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Gain Exp
    public void GainExperience(int amount)
    {
        // Exp Inrease
        currentExp += amount;

        // Loops for Level up 
        while (currentExp >= expToLevel)
        {
            LevelUp();
        }
        UpdateUI();
    }

    // Level Up
    private void LevelUp()
    {
        level++; 
        currentExp -= expToLevel; 
        expToLevel = Mathf.RoundToInt( expToLevel * expGrowthMultiplier);
    }

    // Update UI
    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp; 
        currentLevelText.text = "Level " + level;
    }
}
