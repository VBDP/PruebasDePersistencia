using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1manager : MonoBehaviour
{
public TextMeshProUGUI PlayerNameText;
public TextMeshProUGUI PlayerAgeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        String playerName = PlayerPrefs.GetString("PlayerName", "Guest");
        PlayerNameText.text += " " + playerName;
        int playerAge = PlayerPrefs.GetInt("PlayerAge", 0);
        PlayerAgeText.text += " " + playerAge;

    }

    // Update is called once per frame
    void Update()
    {
        
    }  
}