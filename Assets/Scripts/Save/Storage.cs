using System.IO;
using UnityEngine;

public static class Storage
{
    public static T Load<T>(string fileName)
    {
        string filePath = GetFilePath(fileName);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            
            return JsonUtility.FromJson<T>(json);
        }
        else 
        {
            Debug.LogWarning("Save file not found: " + filePath);
            return default(T);
        }
    }

    public static void Save<T>(T data, string fileName) 
    {
        string filePath = GetFilePath(fileName);
        EnsureDirectoryExists(filePath);

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(GetFilePath(fileName), json);
    }

    private static void EnsureDirectoryExists(string fileName)
    {
       string directoryPath = Path.GetDirectoryName(fileName);

        if (!Directory.Exists(directoryPath)) 
            Directory.CreateDirectory(directoryPath);
    }

    private static string GetFilePath(string fileName) 
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }
}
