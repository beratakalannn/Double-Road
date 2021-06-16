using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    private GameData gameData;
    [HideInInspector]
    public int starScore, scoreCount, selectedIndex;

    [HideInInspector]
    public bool[] heroes;

    [HideInInspector]
    public bool playSound = true;

    private string data_Path = "GameData.dat";


    private void Awake()
    {
        MakeSingleton();
        InitializeGameData();
    }

    private void Start()
    {
        print(Application.persistentDataPath + data_Path);
    }


    void MakeSingleton()
    {
        if (!instance == null)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void InitializeGameData()
    {
        LoadGameData();

        if (gameData == null)
        {
            starScore = 0;
            scoreCount = 0;
            selectedIndex = 0;
            heroes = new bool[6];
            heroes[selectedIndex] = true;

            for (int i = 1; i < heroes.Length; i++)
            {
                heroes[i] = false;
            }
            gameData = new GameData();
            gameData.StarScore = starScore;
            gameData.ScoreCount = scoreCount;
            gameData.Heroes = heroes;
            gameData.SelectIndex = selectedIndex;


            SaveGameData();
        }
    }

    public void SaveGameData()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + data_Path);
            if (gameData != null)
            {
                gameData.Heroes = heroes;
                gameData.StarScore = starScore;
                gameData.ScoreCount = scoreCount;
                gameData.SelectIndex = selectedIndex;

                bf.Serialize(file, gameData);
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    void LoadGameData()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + data_Path, FileMode.Open);

            gameData = (GameData)bf.Deserialize(file);

            if (gameData != null)
            {
                starScore = gameData.StarScore;
                scoreCount = gameData.ScoreCount;
                heroes = gameData.Heroes;
                selectedIndex = gameData.SelectIndex;
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (file != null)
            {
                file.Close(); 
            }
        }
    }

    
}
