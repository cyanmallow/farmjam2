using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class DayMonthManager : MonoBehaviour
{
    public static DayMonthManager Instance { get; private set; }

    public int currentDay = 1;

    // display day on UI
    public TextMeshProUGUI dateUI;
    public TextMeshProUGUI timeUI;
    private float lastTimeOfDay;

    public static event Action OnNewDay;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        dateUI.text = "Day " + currentDay.ToString();
        lastTimeOfDay = LightingManager.Instance.TimeOfDay;
    }

    void Update()
    {
        float currentTimeOfDay = LightingManager.Instance.TimeOfDay;

        if (currentTimeOfDay < lastTimeOfDay)
        {
            NewDay();
        }
        lastTimeOfDay = currentTimeOfDay;

        int hour = Mathf.FloorToInt(LightingManager.Instance.TimeOfDay);
        int minute = Mathf.FloorToInt((LightingManager.Instance.TimeOfDay - hour) * 60f);
        timeUI.text = $"{hour:00}:{minute:00}";
    }

    private void NewDay()
    {
        currentDay++;
        ScreenFader.Instance.FadeFromBlack();
        LightingManager.Instance.TimeOfDay = 5f;
        //newDayUI.SetActive(true);
        dateUI.text = "Day " + currentDay.ToString();
        Debug.Log("New Day: " + currentDay);
        OnNewDay?.Invoke();
    }

    public void AddTime(float timeToAdd)
    {
        LightingManager.Instance.TimeOfDay += timeToAdd;
    }

}
