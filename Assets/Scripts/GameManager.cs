using System;
using UnityEngine;

[Serializable]
public struct PersistantData
{
    public int highScore;
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private PersistantData _persistantData;
    public PersistantData PersistantData => _persistantData;

    public int Score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
        LoadPersistantData();
    }

    private void LoadPersistantData()
    {
        //highScore
        _persistantData.highScore = PlayerPrefs.GetInt(nameof(_persistantData.highScore), 0);
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetInt(nameof(_persistantData.highScore), _persistantData.highScore);
        PlayerPrefs.Save();
    }

    public void RestartScene()
    {
        SaveHighScore();
    }
}
