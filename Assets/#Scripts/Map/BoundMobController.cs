using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundMobController : Enemy
{
    protected override IEnumerator ChangePos()
    {
        canMove = false;
        Vector3 newPos;
        // Gestion des déplacements

        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        while (true)
        {
            if (!GameManager.Instance.IsRunning())
            {
                yield return new WaitUntil(() => GameManager.Instance.IsRunning());
            }
            newPos = transform.position + Time.fixedDeltaTime * speed * (Vector3)direction;
            if (!UniversalSpawner.IsInTheZone(newPos))
            {
                break;
            }
            transform.position = newPos;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForEndOfFrame();
        canMove = true;
    }
}
