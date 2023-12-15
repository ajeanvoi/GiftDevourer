using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_ThunderBolt : AbstractPowerUp
{
    public int nbToKill = 2;

    protected override void Awake()
    {
        target = GameObject.Find("MobsSpawnerController").transform; // On prend le spawner des spawners
        spawner = GetComponentInParent<UniversalSpawner>();
        nbParties = 4;
    }

    protected override void ApplyPower()
    {
        UniversalSpawner[] spawners = target.gameObject.GetComponentsInChildren<UniversalSpawner>();

        SoundController.Instance.MakeThunderSound();

        // On mélange l'ordre des spawners
        spawners = MethodsDO.MelangeTab(spawners);

        int nbKilled = 0;
        foreach (UniversalSpawner spawner in spawners)
        {
            if (nbKilled >= nbToKill)
            {
                break;
            }

            Enemy[] mobs = spawner.GetComponentsInChildren<Enemy>();
            for (int i = 0; i < mobs.Length; i++)
            {
                if (nbKilled >= nbToKill)
                {
                    break;
                }
                if (mobs[i].gameObject.activeInHierarchy)
                {
                    Debug.Log("Kill");
                    spawner.Kill(mobs[i].gameObject);
                    nbKilled++;
                }
            }
        }
    }
}
