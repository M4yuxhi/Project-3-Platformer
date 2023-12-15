using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Globals
{
    private static int goldCoinsCollected;
    private static int maxGoldCoinCount;
    private static bool pause;

    public static int MaxGoldCoinCount { get => maxGoldCoinCount; set => maxGoldCoinCount = value; }
    public static int GoldCoinsCollected { get => goldCoinsCollected; set => goldCoinsCollected = value; }
    public static bool Pause { get => pause; }

    private static void AdjustTimeScale()
    {
        if (Globals.pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public static void ChangeConditionPause()
    {
        pause = !pause;
        AdjustTimeScale();
    }

    public static void SetPause(bool value)
    {
        pause = value;
        AdjustTimeScale();
    }

    private static void CheckDirectory(string directoryName)
    {
        string directoryPath = Application.dataPath + "/" + directoryName;

        string fullPath = Path.Combine(Directory.GetCurrentDirectory(), directoryPath);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(fullPath);
        }
    }

    public static void SaveData()
    {
        CheckDirectory("/Saves");

        GameData data = new GameData();
        data.maxGoldCoinCount = maxGoldCoinCount;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/Saves/gameSave.json", json);
    }

    public static void LoadData()
    {
        CheckDirectory("/Saves");

        string filePath = Application.dataPath + "/Saves/gameSave.json";

        try 
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                GameData data = JsonUtility.FromJson<GameData>(json);
                maxGoldCoinCount = data.maxGoldCoinCount;
            }
            else SaveData();
        }
        catch (Exception e)
        {
            Debug.LogError("Error al cargar el archivo JSON: " + e.Message);
        }
    }

    public static void EraseData()
    {
        CheckDirectory("/Saves");

        GameData data = new GameData();
        data.maxGoldCoinCount = 0;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/Saves/gameSave.json", json);
    }
}
