using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;

public class MainMenuManager : MonoBehaviour
{
    public TMP_InputField playerNameInputField;
    public TMP_InputField playerAgeInputField;
    public TextMeshProUGUI AdvertenciaText;
    public Button saveButton;
    public Button playButton;
    public Button deleteMeButton;
    public Toggle Toggle;

    private string dataFilePath;
    private bool SaveData = false;

   void Start()
{
    dataFilePath = Path.Combine(Application.persistentDataPath, "playerdata.json");
    EnsureDataFileExists();
    saveButton.onClick.AddListener(OnSaveButtonClicked);
    playButton.onClick.AddListener(LoadLevel1);
    deleteMeButton.onClick.AddListener(ForgetMe);

    // Habilitar los campos de entrada al inicio
    playerNameInputField.enabled = true;
    playerAgeInputField.enabled = true;
    saveButton.enabled = true;
    deleteMeButton.enabled = false;

    if (File.Exists(dataFilePath))
    {
        try
        {
            string json = File.ReadAllText(dataFilePath);
            PlayerData data = JsonConvert.DeserializeObject<PlayerData>(json);
            if (data != null)
            {
                playerNameInputField.text = data.playerName;
                playerAgeInputField.text = data.playerAge.ToString();
                Toggle.isOn = data.SaveData;

                // Deshabilitar solo si hay datos guardados
                if (data.SaveData)
                {
                    playerNameInputField.enabled = false;
                    playerAgeInputField.enabled = false;
                    saveButton.enabled = false;
                    deleteMeButton.enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Error leyendo JSON: " + ex.Message);
        }
    }
}

    public void OnSaveButtonClicked()
    {
        if (!int.TryParse(playerAgeInputField.text, out int age))
        {
            AdvertenciaText.text = "Edad no válida.";
            return;
        }

        SaveData = Toggle.isOn;
        PlayerData data = new PlayerData
        {
            playerName = playerNameInputField.text,
            playerAge = age,
            SaveData = SaveData
        };

        if (SaveData)
        {
            try
            {
                File.WriteAllText(dataFilePath, JsonConvert.SerializeObject(data));
                deleteMeButton.enabled = true;
                Debug.Log("La información se ha guardado en JSON");
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Error guardando JSON: " + ex.Message);
            }
        }
        else
        {
            if (File.Exists(dataFilePath)) File.Delete(dataFilePath);
            deleteMeButton.enabled = false;
        }

        playerNameInputField.enabled = false;
        playerAgeInputField.enabled = false;
        saveButton.enabled = false;
    }

    public void ForgetMe()
    {
        if (File.Exists(dataFilePath)) File.Delete(dataFilePath);
        playerNameInputField.enabled = true;
        playerAgeInputField.enabled = true;
        playerNameInputField.text = "";
        playerAgeInputField.text = "";
        saveButton.enabled = true;
        deleteMeButton.enabled = false;
    }

    public void LoadLevel1()
    {
        if (!string.IsNullOrEmpty(playerNameInputField.text) && !string.IsNullOrEmpty(playerAgeInputField.text))
            SceneManager.LoadScene("Level1");
        else
            AdvertenciaText.text = "Por favor, ingresa tu nombre y edad antes de jugar.";
    }

    // helper: crea carpeta/archivo por defecto si hace falta
private void EnsureDataFileExists()
{
    string dir = Path.GetDirectoryName(dataFilePath);
    if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
        Directory.CreateDirectory(dir);

    if (!File.Exists(dataFilePath))
    {
        var defaultData = new PlayerData { playerName = "", playerAge = 0, SaveData = false };
        string json = JsonConvert.SerializeObject(defaultData, Newtonsoft.Json.Formatting.Indented);
        try
        {
            File.WriteAllText(dataFilePath, json);
            Debug.Log($"[MainMenuManager] JSON creado: {dataFilePath}");
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"[MainMenuManager] No se pudo crear JSON: {ex.Message}");
        }
    }
}

// helper: uso centralizado para guardar
private void SavePlayerDataToFile(PlayerData data)
{
    string dir = Path.GetDirectoryName(dataFilePath);
    if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
        Directory.CreateDirectory(dir);

    string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
    File.WriteAllText(dataFilePath, json);
}
}