using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public TMP_InputField playerNameInputField;
    public TMP_InputField playerAgeInputField;
    public TextMeshProUGUI AdvertenciaText;
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
            playerAgeInputField.text = PlayerPrefs.GetInt("PlayerAge", 0).ToString();
            saveButton.enabled = false;
        }
        else {
            Debug.Log("No hay datos de usuario");
            saveButton.enabled = true;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(playerNameInputField.isFocused)
            {
                playerAgeInputField.Select();
            }
            else if(playerAgeInputField.isFocused)
            {
                Toggle.Select();
            }
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
            
            Debug.Log("La informaci�n se ha guardado en el sistema");
            deleteMeButton.enabled = true;
            PlayerPrefs.SetInt("DataSaved", 1);
            PlayerPrefs.Save();
        }
        else
        {
            deleteMeButton.enabled = false;
            Debug.Log("La informaci�n se guardar� temporalmente");
            PlayerPrefs.SetInt("DataSaved", 0);
            
        }
        

        playerNameInputField.enabled = false;
        playerAgeInputField.enabled = false;
        saveButton.enabled = false;

    }

    public void ForgetMe()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Deleted all player data");
        PlayerPrefs.Save();
        playerNameInputField.enabled = true;
        playerAgeInputField.enabled = true;
        playerNameInputField.text = "";
        playerAgeInputField.text = "";
        saveButton.enabled = true;
    }

   public void LoadLevel1()
    {
        if (playerNameInputField.text != "" && playerAgeInputField.text != "")
        {
            SceneManager.LoadScene("Level1");
        }
        else
        {
            AdvertenciaText.text = "Por favor, ingresa tu nombre y edad antes de jugar.";
        }
    }   
}
