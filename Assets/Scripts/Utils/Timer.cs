using UnityEngine;

public class Timer
{
    private float cooldownTime;
    private float currentTime;

    public Timer(float cooldownTime)
    {
        this.cooldownTime = cooldownTime;
        currentTime = cooldownTime;
    }

    public void SubstractTime(float time)
    {
        if(currentTime > 0)
        {
            currentTime -= time;
        }
    }

    public bool TimerCompleted()
    {
        return currentTime <= 0;
    }

    public void ResetTimer()
    {
        currentTime = cooldownTime;
    }
}
