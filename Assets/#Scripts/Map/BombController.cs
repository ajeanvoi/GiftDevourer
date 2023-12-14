using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] private GameObject particleObj;
    [SerializeField] private float raySize = 1f;

    private IEnumerator Start()
    {
        IEnumerator enumerator = CoroutineUtils.WaitSecondsSpecial(this, 1f, () => GameManager.Instance.IsRunning());
        while (enumerator.MoveNext())
        {
            yield return null;
        }
        //particle.Stop();
        GameObject obj = Instantiate(particleObj, transform.position, Quaternion.identity);
        ParticleSystem particleSystem = obj.GetComponent<ParticleSystem>();

        // Calcul de la durée de vie en fonction de la distance maximale
        var mainModule = particleSystem.main;
        float particleLifetime = raySize / (2*mainModule.startSpeed.constant); // Utilisez startSpeed à la place

        // Ajustement de la durée de vie des particules en utilisant startLifetime
        mainModule.startLifetime = particleLifetime;
        Debug.Log(particleLifetime);

        particleSystem.Play();
        CheckMobsTouched();

        // Attendre la fin de la durée de vie des particules
        yield return new WaitForSeconds(particleLifetime);

        Destroy(obj);
    }

    private void CheckMobsTouched()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, raySize);
        Debug.Log(transform.position);

        foreach(Collider2D collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.Kill();
                Debug.Log("Kill !");
            }
        }
        
    }
}
