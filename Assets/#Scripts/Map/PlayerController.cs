using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : Entities
{
    private float moveX = 0f , moveY = 0f;
    private Sprite sprite;
    private float halfWidth, halfHeight;

    [SerializeField] private GameObject bomb;
    [SerializeField] private int nbBomb = 0;
    private TextMeshProUGUI textMeshPro;


    protected override void Start()
    {
        base.Start();
        speed = 3f;
        sprite = GetComponent<SpriteRenderer>().sprite;
        halfWidth = sprite.bounds.size.x / (2 * sprite.pixelsPerUnit * transform.localScale.x);
        halfHeight = sprite.bounds.size.y / (2 * sprite.pixelsPerUnit * transform.localScale.y);

        if (textMeshPro == null)
        {
            textMeshPro = GameObject.Find("BombText").GetComponent<TextMeshProUGUI>();
        }
    }

    public void AddBomb(int nbAddBomb)
    {
        nbBomb += nbAddBomb;
    }

    protected override void OnEnable()
    {
        return;
    }

    // Update is called once per frame
    void Update()
    {
        // Gestion des Inputs
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        if (GameManager.Instance.IsRunning())
        {
            if (nbBomb > 0 && Input.GetKeyDown(KeyCode.Space))
            {
                DropBomb();
            }
        }
    }

    public void DropBomb()
    {
        Debug.Log("Spawn de la bombe");
        GameObject bombInstance = Instantiate(bomb, transform.parent);
        StartCoroutine(KillWithDelay(2f, bombInstance));
        bombInstance.transform.position = transform.position;
        nbBomb--;
        UpdateTextBomb();

    }

    public IEnumerator KillWithDelay(float time, GameObject objectToDestroy)
    {
        IEnumerator enumerator = CoroutineUtils.WaitSecondsSpecial(this, time, () => GameManager.Instance.IsRunning());
        while (enumerator.MoveNext())
        {
            yield return null;
        }
        Destroy(objectToDestroy);
        yield return null;
    }

    public void UpdateTextBomb()
    {
        textMeshPro.text = "Bombs : " + nbBomb;
    }

    protected override void Move()
    {
        Vector2 direction = new Vector2(moveX, moveY).normalized;
        Vector2 newPos = rb.position + Time.fixedDeltaTime * speed * direction;
        //if (UniversalSpawner.IsInTheZoneShape(newPos, halfWidth, halfHeight))
        //{
        //    rb.MovePosition(newPos);
        //}

        if (UniversalSpawner.IsInTheZone(newPos))
        {
            rb.MovePosition(newPos);
        }

        if (!direction.Equals(Vector2.zero))
        {
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            rb.MoveRotation(angle);
        }
        
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        //base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Cadeau"))
        {
            SoundController.Instance.MakeGetGiftSound();
            collision.gameObject.GetComponent<CadeauController>().Kill();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag("Cadeau"))
        {
            SoundController.Instance.MakeGetGiftSound();
            collision.gameObject.GetComponent<CadeauController>().Kill();
        }
    }
}
