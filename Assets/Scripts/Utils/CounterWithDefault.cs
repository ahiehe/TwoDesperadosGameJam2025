using UnityEngine;

public class CounterWithDefault
{
    private int defaultValue;
    private int curValue;

    public CounterWithDefault(int defaultVal)
    {
        defaultValue = defaultVal;
        curValue = defaultVal;
    }

    public bool CurrentValueBigerThanZero()
    {
        return curValue > 0; 
    }

    public void ChangeCounter(int value)
    {
        curValue += value;
    }

    public void SetDefault(int newVal)
    {
        defaultValue = newVal;
        curValue = 0;
    }

    public void SetToDefault()
    {
        curValue = defaultValue;
    }
}
