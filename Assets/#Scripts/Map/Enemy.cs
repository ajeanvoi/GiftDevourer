using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entities
{
    [SerializeField] protected Transform target;
    [SerializeField] protected int nbParties = 3;
    UniversalSpawner spawner;

    protected override void Start()
    {
        OnEnable();
        spawner = GetComponentInParent<UniversalSpawner>();
    }

    protected override void OnEnable()
    {
        if (target == null)
        {
            target = GameObject.Find("Player").transform;
        }

        transform.position = MethodsDO.ChooseSpawnPosition(target, nbParties);
        this.Init();
    }

    protected override void Move()
    {
        StartCoroutine(ChangePos());
    }

    protected abstract IEnumerator ChangePos();

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundController.Instance.MakeKillSound();
            GameManager.Instance.EndGame();
        }
    }

    public void Kill()
    {
        spawner.Kill(this.gameObject);
    }
}
