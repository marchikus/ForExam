using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Saver : MonoBehaviour
{
    private string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    public void SaveGame(SaveData saveData)
    {
        Debug.Log("Сохранение удалось");
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(SavePath, json);
    }

    public SaveData LoadGame()
    {
        if (!File.Exists(SavePath))
        {
            return null;
        }
        Debug.Log("Загрузка удалась");
        string json = File.ReadAllText(SavePath);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public void DeleteSaveFile()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
        }
    }

}
