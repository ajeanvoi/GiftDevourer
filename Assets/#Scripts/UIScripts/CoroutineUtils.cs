using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineUtils
{
    public static IEnumerator WaitSecondsSpecial(MonoBehaviour monoBehaviour, float time, Func<bool> condition)
    {
        IEnumerator enumerator = WaitSpecial(time, condition);
        monoBehaviour.StartCoroutine(enumerator);
        return enumerator;
    }

    private static IEnumerator WaitSpecial(float timeToWait, Func<bool> condition)
    {
        float timePassed = 0;
        while (timePassed < timeToWait)
        {
            if (!condition())
            {
                yield return new WaitUntil(() => condition());
                continue;
            }
            timePassed += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }
}
