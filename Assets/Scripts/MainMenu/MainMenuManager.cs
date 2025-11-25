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
    public Button deleteMeButton;
    public Toggle Toggle;
    public bool SaveData = false;

    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("PlayerName"));
        saveButton.onClick.AddListener(OnSaveButtonClicked);
        playButton.onClick.AddListener(LoadLevel1);
        deleteMeButton.onClick.AddListener(ForgetMe);


        if (PlayerPrefs.GetString(key: "PlayerName") != "")
        {
            Debug.Log("Datos encontrados");
            playerNameInputField.enabled = false;
            playerAgeInputField.enabled = false;
            playerNameInputField.text = PlayerPrefs.GetString(key: "PlayerName");
            playerAgeInputField.text = PlayerPrefs.GetString(key: "PlayerAge");
            saveButton.enabled = false;
        }
        else {
            Debug.Log("No hay datos de usuario");
            saveButton.enabled = true;
        }
    }

public void OnSaveButtonClicked()
    {
        
        String playerName = playerNameInputField.text;
        playerAge = Int32.Parse(playerAgeInputField.text);
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("PlayerAge", playerAge);

        SaveData = Toggle.isOn;
        if (SaveData)
        {
            PlayerPrefs.Save();
            Debug.Log("La información se ha guardado en el sistema");
        }
        else
        {
            Debug.Log("La información se guardará temporalmente");
        }

        playerNameInputField.enabled = false;
        playerAgeInputField.enabled = false;
        saveButton.enabled = false;

    }

    public void ForgetMe()
    {
                PlayerPrefs.DeleteAll();
        Debug.Log("Deleted all player data");
        playerNameInputField.enabled = true;
        playerAgeInputField.enabled = true;
        playerNameInputField.text = "";
        playerAgeInputField.text = ""
            ;
        saveButton.enabled = true;
    }

   public void LoadLevel1()
    {
        SceneManager.LoadSceneAsync("Level1");
    }   
}
