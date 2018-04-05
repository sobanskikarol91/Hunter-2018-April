using UnityEngine;
using System.Collections;

public class ExpiryColor
{
    static Color expiryColor = new Color32(0, 0, 0, 40);

    public static IEnumerator ExpirySpriteColor(SpriteRenderer sprite, float timeTakenDuringLerp = 1f)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted, percentageComplete;
        Color32 startColor = sprite.color;

        do
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / timeTakenDuringLerp;
            sprite.color = Color32.Lerp(startColor, expiryColor, percentageComplete);
            yield return null;

        } while ((percentageComplete < 1));
    }
}
