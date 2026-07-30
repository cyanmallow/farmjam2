using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class GameSaveData
{
    public StatData stats;
    public int currentDay;
    public float timeOfDay;
}
public static class SaveSystem
{
    private const string FileName = "gamesave.json";

    public static void SaveGame(StatManager statManager, DayMonthManager dayMonthManager, LightingManager lightingManager)
    {
        GameSaveData data = new GameSaveData
        {
            stats = new StatData(statManager),
            currentDay = dayMonthManager.currentDay,
            timeOfDay = lightingManager.TimeOfDay
        };
        string path = Application.persistentDataPath + "/" + FileName;
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);

        Debug.Log("Game saved to " + path);
    }
    public static GameSaveData LoadGame()
    {
        string path = Application.persistentDataPath + "/" + FileName;
        if (!File.Exists(path))
        {
            Debug.Log("Save file not found: " + path);
            return null;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<GameSaveData>(json);
    }

    public static void ApplyLoadedData(GameSaveData data, StatManager statManager, DayMonthManager dayMonthManager, LightingManager lightingManager)
    {
        if (data == null) return;

        data.stats.ApplyTo(statManager);
        dayMonthManager.currentDay = data.currentDay;
        lightingManager.TimeOfDay = data.timeOfDay;
    }
}

