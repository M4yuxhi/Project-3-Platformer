using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Globals
{
    public static class Sound
    {
        private static float volMaster = 0.5f;
        private static float volMusic = 0.5f;
        private static float volFX = 0.5f;

        public static float VolMaster { get => volMaster; set => volMaster = value; }
        public static float VolMusic { get => volMusic; set => volMusic = value; }
        public static float VolFX { get => volFX; set => volFX = value; }
    }

    public static class Saves
    {
        private static readonly int maxSavesSlotNumber = 3;
        private static int selectedSaveSlot = 1;

        public static int MaxSavesSlotNumber { get => maxSavesSlotNumber; }
        public static int SelectedSavesSlot { get => selectedSaveSlot; set => selectedSaveSlot = value; }

        private static void CheckDirectory(string directoryName)
        {
            string directoryPath = Application.dataPath + "/" + directoryName;

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), directoryPath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(fullPath);
            }
        }

        public static void SaveData(int slot)
        {
            CheckDirectory("/Saves");

            GameData data = new GameData();
            data.maxGoldCoinCount = maxGoldCoinCount;

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(Application.dataPath + "/Saves/gameSave" + slot + ".json", json);
        }

        public static void LoadData(int slot)
        {
            CheckDirectory("/Saves");

            string filePath = Application.dataPath + "/Saves/gameSave" + slot + ".json";

            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    GameData data = JsonUtility.FromJson<GameData>(json);
                    maxGoldCoinCount = data.maxGoldCoinCount;
                }
                else SaveData(slot);
            }
            catch (Exception e)
            {
                Debug.LogError("Error al cargar el archivo JSON: " + e.Message);
            }

            SelectedSavesSlot = slot;
        }

        public static void EraseData(int slot)
        {
            CheckDirectory("/Saves");

            GameData data = new GameData();
            data.maxGoldCoinCount = 0;

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(Application.dataPath + "/Saves/gameSave" + slot + ".json", json);

            if (slot == SelectedSavesSlot) LoadData(slot);
        }
    }

    private static int goldCoinsCollected;
    private static int maxGoldCoinCount;
    private static bool pause;

    public static int GoldCoinsCollected { get => goldCoinsCollected; set => goldCoinsCollected = value; }
    public static int MaxGoldCoinCount { get => maxGoldCoinCount; set => maxGoldCoinCount = value; }
    public static bool Pause { get => pause; }

    private static void AdjustTimeScale()
    {
        if (pause)
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

    
}