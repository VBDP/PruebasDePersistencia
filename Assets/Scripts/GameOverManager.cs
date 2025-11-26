using System;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json; // add json support

public class GameOverManager : MonoBehaviour
{
    float timer = 0;
    public TextMeshProUGUI gameOverText;

    void Start()
    {
    }

    void Update()
    {
        timer += Time.deltaTime;
        gameOverText.text = "Game Over! Exiting in " + (5 - timer).ToString("F2") + " seconds.";
        Debug.Log(timer);
        if (timer >= 5f)
        {
            // delete json if SaveData is false or file is invalid
            TryDeleteJsonIfNotSaved();

            Application.Quit();
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #endif
        }
    }

    private void TryDeleteJsonIfNotSaved()
    {
        string dataFilePath = Path.Combine(Application.persistentDataPath, "playerdata.json");
        if (!File.Exists(dataFilePath)) return;

        try
        {
            string json = File.ReadAllText(dataFilePath);
            PlayerData pd = JsonConvert.DeserializeObject<PlayerData>(json);

            // if we couldn't parse or SaveData == false -> delete file
            if (pd == null || pd.SaveData == false)
            {
                File.Delete(dataFilePath);
                Debug.Log($"[GameOverManager] Deleted JSON: {dataFilePath}");
            }
            else
            {
                Debug.Log("[GameOverManager] SaveData == true, keeping JSON.");
            }
        }
        catch (Exception ex)
        {
            // if anything goes wrong, remove the file to avoid corrupt leftovers
            try { File.Delete(dataFilePath); } catch { /* ignore */ }
            Debug.LogWarning($"[GameOverManager] Error reading/deleting JSON: {ex.Message}");
        }
    }
}
