using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceController : Enemy
{
    [SerializeField] private float detectRange = 4f;

    protected override IEnumerator ChangePos()
    {
        canMove = false;

        float timeToMove = Random.Range(1f, 2f);
        float startTime = Time.time;
        float currentTimePassed = 0;

        // Gestion des déplacements

        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        while (currentTimePassed < timeToMove)
        {
            Vector3 newPos;
            // Check if is near the monster
            if (target != null)
            {
                currentTimePassed = Time.time - startTime;
                if (!GameManager.Instance.IsRunning())
                {
                    float pausingTime = Time.time;
                    yield return new WaitUntil(() => GameManager.Instance.IsRunning());
                    currentTimePassed -= pausingTime;
                }

                float dist = Mathf.Abs(Vector2.Distance(this.transform.position, target.position));
                if (dist < detectRange)
                {
                    direction = ((Vector2)target.position- (Vector2)this.transform.position).normalized;
                }

                newPos = transform.position + Time.fixedDeltaTime * speed * (Vector3)direction;

                if (UniversalSpawner.IsInTheZone(newPos))
                {
                    transform.position = newPos;
                }
            }
            
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForEndOfFrame();
        canMove = true;
    }
}
