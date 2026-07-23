using System;
using UnityEngine;

public class Stat
{
    public string statId;
    public float currentValue;
    public float maxValue;
    public float decayOrIncreasePerHour;

    public event Action<float> OnChanged;

    public void Add(float amount)
    {
        currentValue = Mathf.Clamp(currentValue + amount, 0, maxValue);
        OnChanged?.Invoke(currentValue);
    }

    public void Lose(float amount)
    {
        currentValue = Mathf.Clamp(currentValue - amount, 0, maxValue);
        OnChanged?.Invoke(currentValue);
    }

    public void Tick()
    {
        currentValue = Mathf.Clamp(currentValue + decayOrIncreasePerHour, 0, maxValue);
        OnChanged?.Invoke(currentValue);
    }
}
