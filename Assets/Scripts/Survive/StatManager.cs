using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance { get; private set; }
    public Dictionary<string, Stat> stats = new();

    void Awake()
    {
        Instance = this;
        stats["hunger"] = new Stat { statId = "hunger", currentValue = 0, maxValue = 100, decayOrIncreasePerHour = 5f };
        stats["fatigue"] = new Stat { statId = "fatigue", currentValue = 0, maxValue = 100, decayOrIncreasePerHour = 2f };
        stats["pain"] = new Stat { statId = "pain", currentValue = 0, maxValue = 100, decayOrIncreasePerHour = -5f };
        stats["sanity"] = new Stat { statId = "sanity", currentValue = 100, maxValue = 100, decayOrIncreasePerHour = 0f };
    }

    public Stat Get(string id) => stats[id];

    public void TickStatsEveryHour()
    {
        foreach (var stat in stats.Values) stat.Tick();
    }

    public void AddStat(string id, float amount)
    {
        if (stats.ContainsKey(id))
        {
            stats[id].Add(amount);
            Debug.Log($"Added {amount} to stat '{id}'. New value: {stats[id].currentValue}");
        }
        else
        {
            Debug.LogWarning($"Stat with ID '{id}' does not exist.");
        }
    }

    public void LoseStat(string id, float amount)
    {
        if (stats.ContainsKey(id))
        {
            stats[id].Lose(amount);
            Debug.Log($"Lost {amount} from stat '{id}'. New value: {stats[id].currentValue}");
        }
        else
        {
            Debug.LogWarning($"Stat with ID '{id}' does not exist.");
        }
    }
    public void CompareStat(string id, float amount)
    {
        if (stats.ContainsKey(id))
        {
            float currentValue = stats[id].currentValue;
            if (currentValue < amount)
            {
                Debug.Log($"Stat '{id}' is less than {amount}. Current value: {currentValue}");
            }
            else if (currentValue > amount)
            {
                Debug.Log($"Stat '{id}' is greater than {amount}. Current value: {currentValue}");
            }
            else
            {
                Debug.Log($"Stat '{id}' is equal to {amount}. Current value: {currentValue}");
            }
        }
        else
        {
            Debug.LogWarning($"Stat with ID '{id}' does not exist.");
        }
    }
}
