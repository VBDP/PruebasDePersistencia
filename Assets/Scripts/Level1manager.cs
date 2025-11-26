using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json; // <-- añadido

public class Level1manager : MonoBehaviour
{
    public TextMeshProUGUI PlayerNameText;
    public TextMeshProUGUI PlayerAgeText;
    public Button MainMenuButton;
    public Button QuitButton;
    public Button Level2Button;

    void Start()
    {
        // intenta leer JSON guardado
        string dataFilePath = Path.Combine(Application.persistentDataPath, "playerdata.json");
        bool loadedFromJson = false;

        if (File.Exists(dataFilePath))
        {
            try
            {
                string json = File.ReadAllText(dataFilePath);
                PlayerData pd = JsonConvert.DeserializeObject<PlayerData>(json);
                if (pd != null)
                {
                    // usa los datos del JSON
                    PlayerNameText.text += " " + (string.IsNullOrEmpty(pd.playerName) ? "Guest" : pd.playerName);
                    PlayerAgeText.text += " " + pd.playerAge;
                    loadedFromJson = true;
                    Debug.Log($"[Level1manager] Cargado desde JSON: {dataFilePath}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning("[Level1manager] Error leyendo JSON: " + ex.Message);
            }
        }

        // fallback: PlayerPrefs si no hay JSON válido
        if (!loadedFromJson)
        {
            string playerName = PlayerPrefs.GetString("PlayerName", "Guest");
            PlayerNameText.text += " " + playerName;
            int playerAge = PlayerPrefs.GetInt("PlayerAge", 0);
            PlayerAgeText.text += " " + playerAge;
            Debug.Log("[Level1manager] Cargado desde PlayerPrefs (fallback).");
        }

        MainMenuButton.onClick.AddListener(LoadMainMenu);
        QuitButton.onClick.AddListener(ExitGame);
        Level2Button.onClick.AddListener(LoadLevel2);
    }

    void Update() { }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void ExitGame()
    {
        // si no hay JSON con SaveData=true, limpia PlayerPrefs
        string dataFilePath = Path.Combine(Application.persistentDataPath, "playerdata.json");
        bool hasSavedJson = false;
        if (File.Exists(dataFilePath))
        {
            try
            {
                var pd = JsonConvert.DeserializeObject<PlayerData>(File.ReadAllText(dataFilePath));
                hasSavedJson = pd != null && pd.SaveData;
            }
            catch { /* ignorar */ }
        }

        if (!hasSavedJson)
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