using System;
using UnityEngine;


public class DayMonthManager : MonoBehaviour
{
    public static DayMonthManager Instance { get; private set; }

    public int currentDay = 1;
    public float currentTime;
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

    void Update()
    {
        float time = LightingManager.Instance.TimeOfDay;
        if (time < lastTimeOfDay)
        {
            NewDay();
        }
        lastTimeOfDay = time;

        int hour = Mathf.FloorToInt(time);
        int minute = Mathf.FloorToInt((time - hour) * 60f);
        currentTimeUI = $"{hour:00}:{minute:00}";
    }

    private void NewDay()
    {
        currentDay++;
        //ScreenFader.Instance.FadeFromBlack();
        Debug.Log("New Day: " + currentDay);
        OnNewDay?.Invoke();
    }

    public void AddTime(float timeToAdd)
    {
        LightingManager.Instance.TimeOfDay += timeToAdd;
    }

}
