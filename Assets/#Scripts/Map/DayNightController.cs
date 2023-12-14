using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

/// <summary>
/// Gère les lumières avec le cycle jour/nuit
/// </summary>
public class DayNightController : MonoBehaviour
{
    private Light2D globalLight2D;
    private bool isDayOn = true;
    public float dayTime = 20f;
    public float nightTime = 10f;
    [SerializeField] private Light2D playerLight; // Pour gérer l'allumage/extinction de sa lumière
    private float startIntensityLight;

    // Start is called before the first frame update
    void Start()
    {
        startIntensityLight = playerLight.intensity;
        if (isDayOn)
        {
            playerLight.intensity = 0;
        }
        globalLight2D = GetComponent<Light2D>();
        StartCoroutine(ChangeCycle());
    }

    /// <summary>
    /// Méthode pour alterner entre des cycles jours et des cyles nuits
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeCycle()
    {
        while (true)
        {
            if (!GameManager.Instance.IsRunning())
            {
                yield return new WaitUntil(() => GameManager.Instance.IsRunning());
                Debug.Log("Reprise");
            }

            //yield return new WaitForSeconds(dayTime);
            IEnumerator enumerator = CoroutineUtils.WaitSecondsSpecial(this, dayTime, () => GameManager.Instance.IsRunning());
            while (enumerator.MoveNext())
            {
                // On attend que l'enumerator finisse
                yield return null;
            }
            StartCoroutine(ChangeToNight());
            //yield return new WaitForSeconds(nightTime);
            enumerator = CoroutineUtils.WaitSecondsSpecial(this, nightTime, () => GameManager.Instance.IsRunning());
            while (enumerator.MoveNext())
            {
                // On attend que l'enumerator finisse
                yield return null;
            }
            StartCoroutine(ChangeToDay());
            yield return null;
        }
    }

    /// <summary>
    /// Méthode pour passer au jour
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeToDay()
    {
        float startTime = Time.time;
        float duration = 1f;

        float minimum = 0f;
        float maximum = 1f;

        while (globalLight2D.intensity < 1f)
        {
            float t = (Time.time - startTime) / duration;
            globalLight2D.intensity += Mathf.SmoothStep(minimum * Time.deltaTime, maximum * Time.deltaTime, t);
            if (!GameManager.Instance.IsRunning())
            {
                yield return new WaitWhile(() => !GameManager.Instance.IsRunning());
                Debug.Log("Reprise");
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        globalLight2D.intensity = 1f;
        yield return null;
        isDayOn = true;
        playerLight.intensity = 0;
    }

    /// <summary>
    /// Méthode pour passer à la nuit
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeToNight()
    {
        float startTime = Time.time;
        float duration = 1f;

        float minimum = 0f;
        float maximum = 1f;

        playerLight.intensity = startIntensityLight;

        while (globalLight2D.intensity > 0.01f)
        {
            float t = (Time.time - startTime) / duration;
            globalLight2D.intensity -= Mathf.SmoothStep(minimum * Time.deltaTime, maximum * Time.deltaTime, t);
            if (!GameManager.Instance.IsRunning())
            {
                yield return new WaitUntil(() => GameManager.Instance.IsRunning());
                Debug.Log("Reprise");
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        globalLight2D.intensity = 0.01f;
        yield return null;
        isDayOn = false;
    }

    /// <summary>
    /// Méthode pour savoir si le cycle est à jour ou nuit
    /// </summary>
    /// <returns>True si le jour est actif, false sinon</returns>
    public bool IsDayOn()
    {
        return isDayOn;
    }



}
