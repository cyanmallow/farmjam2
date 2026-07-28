using System;
using UnityEngine;

public class DayMonthManager : MonoBehaviour
{
    public static DayMonthManager Instance { get; private set; }

    public int currentDay = 1;
    private float lastTimeOfDay;
    public string currentTimeUI;

    public static event Action OnNewDay;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        lastTimeOfDay = LightingManager.Instance.TimeOfDay;
    }
    public void AddTime(float hoursToAdd)
    {
        LightingManager.Instance.TimeOfDay = (LightingManager.Instance.TimeOfDay + hoursToAdd) % 24f;
        ChangeToNewDayCheck();
    }
    public void ChangeToNewDayCheck()
    {
        float time = LightingManager.Instance.TimeOfDay;
        if (time < lastTimeOfDay)
        {
            NewDay();
        }
        else
        {
            UpdateTimeUI();
        }
        lastTimeOfDay = time;
    }
    private void NewDay()
    {
        currentDay++;
        //ScreenFader.Instance.FadeFromBlack();
        Debug.Log("New Day: " + currentDay);
        OnNewDay?.Invoke();
        UpdateTimeUI();
    }
    public void UpdateTimeUI()
    {
        int hour = Mathf.FloorToInt(LightingManager.Instance.TimeOfDay);
        int minute = Mathf.FloorToInt((LightingManager.Instance.TimeOfDay - hour) * 60f);
        currentTimeUI = $"{hour:00}:{minute:00}";
    }
}
