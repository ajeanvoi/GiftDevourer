using UnityEngine;
using System.Collections;

public class Shaker : MonoBehaviour
{
    /// <summary>
    /// Appeler cette m�thode pour faire trembler le gameobject associ� avec des valeurs pr�d�finies
    /// </summary>
    public void DoShake()
    {
        DoShake(0.5f, 3f);
    }

    /// <summary>
    /// Appeler cette m�thode pour faire trembler le gameobject associ� avec vos propres valeurs
    /// </summary>
    /// <param name="duration">Dur�e du tremblement</param>
    /// <param name="magnitude">D�placement maximal en distance lors du tremblement</param>
    public void DoShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            Vector3 randomOffset = Random.insideUnitSphere * magnitude;
            transform.localPosition = originalPosition + randomOffset;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
