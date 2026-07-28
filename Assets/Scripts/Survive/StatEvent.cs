using UnityEngine;

public class StatEvent : MonoBehaviour
{
    public void CheckPassoutEveryHour()
    {
        if (StatManager.Instance.CompareStat("fatigue", 100))
        {
            Debug.Log("Player has passed out due to fatigue.");
            PassOut();
        }
    }
    public void CheckHungerEveryHour()
    {
        if (StatManager.Instance.CompareStat("hunger", 100))
        {
            Debug.Log("Player has died due to starvation.");
            // Implement dead logic here
        }
    }
    
    public void CheckHealthEveryHour()
    {
        if (StatManager.Instance.CompareStat("pain", 100))
        {
            Debug.Log("Player has died due to poor health.");
            // Implement dead logic here
        }
    }

    // pass out when fatigue gets too high
    public void PassOut()
    {
        int hours = (int)((StatManager.Instance.stats["fatigue"].currentValue - 20) / 7);
        // heal less compared to when player choose to rest
        StatManager.Instance.LoseStat("fatigue", hours * 6);
        DayMonthManager.Instance.AddTime(hours);
        Debug.Log("You have passed out for " + hours + "hours.");
    }

    public void RestToReduceFatiguePain(int howManyHour)
    {
        StatManager.Instance.LoseStat("fatigue", howManyHour * 10);
        StatManager.Instance.LoseStat("pain", howManyHour * 5);
        DayMonthManager.Instance.AddTime(howManyHour);
        Debug.Log("You have rested for " + howManyHour + "hours.");
    }

    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        
    }
}
