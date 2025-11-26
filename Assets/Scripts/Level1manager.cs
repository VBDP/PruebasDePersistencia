using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1manager : MonoBehaviour
{
public TextMeshProUGUI PlayerNameText;
public TextMeshProUGUI PlayerAgeText;
    public Button MainMenuButton;
    public Button QuitButton;
    public Button Level2Button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        String playerName = PlayerPrefs.GetString("PlayerName", "Guest");
        PlayerNameText.text += " " + playerName;
        int playerAge = PlayerPrefs.GetInt("PlayerAge", 0);
        PlayerAgeText.text += " " + playerAge;

        MainMenuButton.onClick.AddListener(LoadMainMenu);
        QuitButton.onClick.AddListener(ExitGame);
        Level2Button.onClick.AddListener(LoadLevel2);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainMenu ()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void ExitGame()
    {
        if(PlayerPrefs.GetInt("DataSaved", 0) == 0)
        {
            PlayerPrefs.DeleteAll();
        }
        
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void LoadLevel2()
    {
        SceneManager.LoadSceneAsync("Level2");
    }
}