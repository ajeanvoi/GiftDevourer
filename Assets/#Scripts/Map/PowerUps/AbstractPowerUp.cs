using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPowerUp : MonoBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] protected int nbParties = 5;
    protected UniversalSpawner spawner;
    [SerializeField] protected float powerTime = 4f;

    protected virtual void Awake()
    {
        target = GameObject.Find("Player").transform;
        spawner = GetComponentInParent<UniversalSpawner>();
    }

    protected void OnEnable()
    {
        Spawn();
    }

    protected virtual void Spawn()
    {
        this.transform.position = MethodsDO.ChooseSpawnPosition(target, nbParties);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyPower();
            spawner.Kill(this.gameObject);
        }
    }

    protected abstract void ApplyPower();
}
