using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string playerName;
    public string playerNameFromHighscore;
    public int highscore;
    public InputField playerNameInputField; 
    
    public UnityEngine.UI.Button StartButton;
    public TextMeshProUGUI noName;

    private void Awake()
    {

        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code
       
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

  
    public void loadGame() {
        if (playerNameInputField.text.Equals(""))
        {
            
            noName.SetText("Please enter player name");
            
        }
        else {
           noName.SetText("");
            
            playerName = playerNameInputField.text;
            SceneManager.LoadScene(1);
        }
    }

    public void saveHighscore(string playername, int highscore) {
        SaveData data = new SaveData();
        data.playerName = playername;
        data.highScore = highscore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }

    public void LoadHighscore() {
        string path = Application.persistentDataPath + "/highscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            playerNameFromHighscore = data.playerName;
            highscore = data.highScore;
        }
        else {

            playerNameFromHighscore = "";
            highscore = 0;
        }


    }
    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int highScore;
    }
}