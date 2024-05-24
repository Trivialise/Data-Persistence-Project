using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ScoreHandler : MonoBehaviour
{
    public static ScoreHandler Instance;
    public string currentName;
    public string highName = "Beat This";
    public TextMeshProUGUI InputName;
    public int highScore = 1;
    public TextMeshProUGUI HighDisplay;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
        HighDisplay.text = $"{highName} - {highScore}";
    }

    public void GetName()
    {
        currentName = InputName.text;
    }
    [System.Serializable]
    class SaveData
    {
        public string highName;
        public int highScore;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highName = highName;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highName = data.highName;
            highScore = data.highScore;
        }
    }
}
