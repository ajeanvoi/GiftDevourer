using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entities : MonoBehaviour
{
    protected bool canMove = true;
    protected Rigidbody2D rb;
    [SerializeField] protected float speed = 1;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        float rdX = Random.Range(UniversalSpawner.xMin, UniversalSpawner.xMax);
        float rdY = Random.Range(UniversalSpawner.yMin, UniversalSpawner.yMax);
        this.transform.position = new Vector3(rdX, rdY, 0);

        this.Init();
    }

    protected virtual void Init()
    {
        canMove = true;
        speed = 1;
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (canMove && GameManager.Instance.IsRunning())
        {
            Move();
        }
    }

    protected abstract void Move();

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision entre " + this.gameObject.name + " et " + collision.gameObject.name);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Collision entre " + this.gameObject.name + " et " + collision.gameObject.name);
    }

    /// <summary>
    /// Augmente la vitesse de 1 lorsqu'appelé
    /// </summary>
    public void BoostSpeed(float amount = 1f)
    {
        speed += amount;
    }

    /// <summary>
    /// Boost temporairement la vitesse
    /// </summary>
    /// <param name="duringTime">Temps du boost</param>
    /// <param name="amount">Valeur du boost</param>
    public void TemporaryBoost(float duringTime, float amount = 1)
    {
        StartCoroutine(DoTemporaryBoost(duringTime, amount));
    }

    protected IEnumerator DoTemporaryBoost(float duringTime, float amount)
    {
        BoostSpeed(amount);
        yield return new WaitForSeconds(duringTime);
        DecreaseSpeed(amount);
    }


    /// <summary>
    /// Méthode qui diminue la vitesse
    /// </summary>
    /// <param name="amount"> De combien on diminue la vitesse, 1 par défaut</param>
    public void DecreaseSpeed(float amount = 1)
    {
        speed -= amount;
    }
}
