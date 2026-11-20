using UnityEngine;

public class StatEvent : MonoBehaviour
{
    public static StatEvent Instance { get; private set; }
    private void Awake() => Instance = this;
    public void CheckPassout()
    {
        if (StatManager.Instance.CompareStat("fatigue", 100))
        {
            Debug.Log("Player has passed out due to fatigue.");
            PassOut();
        }
    }
    public void CheckHunger()
    {
        if (StatManager.Instance.CompareStat("hunger", 100))
        {
            Debug.Log("Player has died due to starvation.");
            // Implement dead logic here
        }
    }
    public void CheckHealth()
    {
        // do check every hour
        if (StatManager.Instance.CompareStat("pain", 100))
        {
            Debug.Log("Player has died due to poor health.");
            // Implement dead logic here
        }
    }
    // pass out when fatigue gets too high
    public void PassOut()
    {
        // passing out will reduce fatigue and pain, but not as much as sleeping
        int hours = (int)((StatManager.Instance.stats["fatigue"].currentValue - 20) / 7);
        StatManager.Instance.LoseStat("fatigue", hours * 6);
        DayMonthManager.Instance.AddTime(hours);
        NotifyUILogic.Instance.DisplayNotification("You have passed out for " + hours + "hours.");
    }

    public void Sleep(int howManyHour)
    {
        // sleeping will reduce fatigue and pain
        StatManager.Instance.LoseStat("fatigue", howManyHour * 10);
        StatManager.Instance.LoseStat("pain", howManyHour * 5);
        DayMonthManager.Instance.AddTime(howManyHour);
        NotifyUILogic.Instance.DisplayNotification("You have rested for " + howManyHour + "hours.");
    }
}
