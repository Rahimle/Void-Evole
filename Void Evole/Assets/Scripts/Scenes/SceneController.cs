using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Function for Buttons
    // Play & Restart
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Home
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Exit
    public void ExitGame()
    {
        Application.Quit();
    }
}
