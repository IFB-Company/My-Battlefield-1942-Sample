using UnityEngine;

public static class FloatExtensions
{
    public static void IncreaseValue(this ref float value, float targetValue, float amount)
    {
        if (targetValue > 0)
        {
            if (value < targetValue)
                value += amount;
            else if (value > -targetValue)
                value -= amount;
        }
        else
        {
            if (value > targetValue)
                value -= amount;
            else if (value < -targetValue)
                value += amount;
        }
    }

    public static void DecreaseValue(this ref float value, float amount)
    {
        if (value > amount)
            value -= amount;
        else if (value < -amount)
            value += amount;
        else 
            value = 0f;
    }
}
