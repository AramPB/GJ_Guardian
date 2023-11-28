using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpFunction : MonoBehaviour
{
    public static float Lerp(float start, float end, float timeStartedLerping, float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;

        float percentageComplete = timeSinceStarted / lerpTime;

        float result = Vector3.Lerp(new Vector3(start, 0, 0), new Vector3(end, 0, 0), percentageComplete).x;

        return result;
    }
}
