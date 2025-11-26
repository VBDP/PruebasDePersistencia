using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    float timer = 0;
    public TextMeshProUGUI gameOverText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
             Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
         #endif
        }
    }

}
