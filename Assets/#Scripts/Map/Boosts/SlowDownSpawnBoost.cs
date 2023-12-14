using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownSpawnBoost : AbstractBoost
{
    SpawnerController mobController;
    [SerializeField]
    [Header("Temps ajouté au temps de spawn de base")]

    private float timeToAdd = 0.5f;

    public override void ApplyPower()
    {
        if (mobController == null)
        {
            mobController = GameObject.Find("MobsSpawnerController").GetComponent<SpawnerController>();
        }
        mobController.timeToWaitAddition += timeToAdd;
    }
}
