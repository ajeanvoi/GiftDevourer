using System.Collections;
using UnityEngine;

public class CadeauController : Entities
{
    UniversalSpawner spawner;

    protected override void Start()
    {
        base.Start();
        spawner = GameObject.Find("SpawnerGift").GetComponent<UniversalSpawner>();
    }

    protected override void Move()
    {
        StartCoroutine(ChangePos());
    }

    protected IEnumerator ChangePos()
    {
        canMove = false;

        float timeToMove = Random.Range(1f,2f);
        float startTime = Time.time;
        float currentTimePassed = 0;

        // Gestion des déplacements

        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        while (currentTimePassed < timeToMove)
        {
            currentTimePassed = Time.time - startTime;
            Vector3 newPos = transform.position + Time.fixedDeltaTime * speed * (Vector3)randomDirection;
            if (!GameManager.Instance.IsRunning())
            {
                float pausingTime = Time.time;
                yield return new WaitUntil(() => GameManager.Instance.IsRunning());
                currentTimePassed -= pausingTime;
            }
            if (UniversalSpawner.IsInTheZone(newPos))
            {
                transform.position = newPos;
            }
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForEndOfFrame();
        canMove = true;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    public void Kill()
    {
        spawner.Kill(this.gameObject);
    }
}
