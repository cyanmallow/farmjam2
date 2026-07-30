using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class StatData 
{
    public List<string> keys = new List<string>();
    public List<Stat> values = new List<Stat>();

    public StatData(StatManager statManager)
    {
        foreach (var kvp in statManager.stats)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }

    public void ApplyTo(StatManager statManager)
    {
        statManager.stats.Clear();
        for (int i = 0; i < keys.Count; i++)
        {
            statManager.stats[keys[i]] = values[i];
        }
    }
}
