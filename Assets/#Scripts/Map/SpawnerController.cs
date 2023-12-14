using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private UniversalSpawner[] spawners;
    public float timeToWaitAddition = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spawners = GetComponentsInChildren<UniversalSpawner>();
        StartCoroutine(SpawnBoost());
        if (!NoSpawnerSpawningContinously())
        {
            Debug.LogWarning("Attention, il y a un spawner en automatique");
        }
    }

    private IEnumerator SpawnBoost()
    {
        while (true)
        {
            if (!GameManager.Instance.IsRunning())
            {
                yield return new WaitUntil(() => GameManager.Instance.IsRunning());
                continue;
            }
            else
            {
                int index = ChooseBonusIndex();
                //yield return new WaitForSeconds(spawners[index].timeBetweenSpawns);
                IEnumerator enumerator = CoroutineUtils.WaitSecondsSpecial(this, spawners[index].timeBetweenSpawns + timeToWaitAddition, () => GameManager.Instance.IsRunning());
                while (enumerator.MoveNext())
                {
                    // On attend que l'enumerator finisse
                    yield return null;
                }
                yield return null;
                spawners[index].Spawn();
            }            
        }        
    }

    private int ChooseBonusIndex()
    {
        return Random.Range(0, spawners.Length);
    }

    /// <summary>
    /// Regarde si tous les spawners sont bien en mode non continu
    /// </summary>
    /// <returns>true si aucun en spawn continu, false</returns>
    private bool NoSpawnerSpawningContinously()
    {
        foreach (UniversalSpawner spawner in spawners)
        {
            if (spawner.isLooping)
            {
                return false;
            }
        }
        return true;
    }
}
