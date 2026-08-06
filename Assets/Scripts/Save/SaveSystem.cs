using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private static GameSaveData pendingLoadData;

    public static void SaveGame()
    {
        GameSaveData data = new GameSaveData
        {
            stats = new StatData(StatManager.Instance),
            currentDay = DayMonthManager.Instance.currentDay,
            timeOfDay = LightingManager.Instance.TimeOfDay
        };
        string path = Application.persistentDataPath + "/" + FileName;
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);

        Debug.Log("Game saved to " + path);
    }

    // Pure read — no scene logic here
    public static GameSaveData LoadGameOnly()
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

    // Entry point buttons should call
    public static void LoadGame()
    {
        GameSaveData data = LoadGameOnly();
        if (data == null) return;

        pendingLoadData = data;
        SceneManager.sceneLoaded += OnGameplaySceneLoaded;

        int targetScene = SceneManager.GetActiveScene().buildIndex == 0
            ? SceneManager.GetActiveScene().buildIndex + 1
            : SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(targetScene);
    }

    private static void OnGameplaySceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnGameplaySceneLoaded; // unsubscribe so it only fires once

        if (pendingLoadData != null)
        {
            ApplyLoadedData(pendingLoadData, StatManager.Instance, DayMonthManager.Instance, LightingManager.Instance);
            pendingLoadData = null;
        }
        FadeScreen.Instance.FadeFromBlack(); // Fade in after loading
    }
}