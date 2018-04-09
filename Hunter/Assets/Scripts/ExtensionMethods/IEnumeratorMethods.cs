using UnityEngine;
using System.Collections;
using System;

public static class IEnumeratorMethods
{
    public static IEnumerator Lerp(float origin, float destiny, float duration, Action<float> OnDuring)
    {
        int currentValue = 0;
        float startTime = Time.time;

        float percent = 0;

        while (percent < 1)
        {
             percent = (Time.time - startTime) / duration;
            currentValue = (int)Mathf.Lerp(0, destiny, percent);
            OnDuring(currentValue);
            yield return null;
        }
    }
}
