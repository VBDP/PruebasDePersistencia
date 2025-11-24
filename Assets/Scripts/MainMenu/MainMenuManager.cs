using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public TMP_InputField playerNameInputField;
    public TMP_InputField playerAgeInputField;
    private String playerName;
    private int playerAge;
    public Button saveButton;
    public Button playButton;

    void Start()
    {
        saveButton.onClick.AddListener(OnSaveButtonClicked);
        playButton.onClick.AddListener(LoadLevel1);
    }

public void OnSaveButtonClicked()
    {
        String playerName = playerNameInputField.text;
        playerAge = Int32.Parse(playerAgeInputField.text);
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("PlayerAge", playerAge);
        PlayerPrefs.Save();
        
    }

   public void LoadLevel1()
    {
        SceneManager.LoadSceneAsync("Level1");
    }   
}
