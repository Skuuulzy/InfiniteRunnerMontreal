using System;
using System.IO;
using UnityEngine;

public static class SaveService
{
    private const string FILE_NAME = "save.json";
    private static string FilePath => Path.Combine(Application.persistentDataPath, FILE_NAME);
    
    public static void Save(SaveData saveData)
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(FilePath, json);
        
        Debug.Log("Player data saved at " + FilePath);
    }

    public static bool TryLoad(out SaveData saveData)
    {
        string json;
        
        try
        {
            json = File.ReadAllText(FilePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Unable to read save file. Details: " + e);
            saveData = null;
            
            return false;
        }

        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("No save data found at path: " + FilePath);
            saveData = null;
            
            return false;
        }
        
        var result = JsonUtility.FromJson<SaveData>(json);
        saveData = result;
        
        return true;
    }
}