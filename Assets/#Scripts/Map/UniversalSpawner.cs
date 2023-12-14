using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class UniversalSpawner : MonoBehaviour
{
    // Infos sur la taille du spawn
    protected RectTransform rectTransform;
    public static float xMin, xMax, yMin, yMax;
    public static Vector3[] corners;

    // Paramètres de spawn
    [Header("Spawn parameters")]
    public float timeBetweenSpawns = 3f;
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected bool canSpawn = true;
    public bool isLooping = true;

    // Faut-il spawn ou non suivant ces paramètres
    [SerializeField] protected bool hasLimitNumber = false;
    [SerializeField] protected int maxSpawn = 10;
    [SerializeField] public int currentEnabled = 0;

    [Header("Points Reward")]
    [SerializeField] protected int pointsToWin = 1;

    //Paramètres du pooling 
    [Header("Pooling parameters")]
    [SerializeField] protected int maxSize = 50;
    [SerializeField] protected int normalSize = 20;
    protected IObjectPool<GameObject> pool;

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        xMin = corners[0].x;
        yMin = corners[0].y;
        xMax = corners[2].x;
        yMax = corners[2].y;

        pool = new ObjectPool<GameObject>(() => { return Instantiate(prefab, transform); }, // How to Instantiate
            obj => { obj.SetActive(true); },
            obj => { obj.SetActive(false); },
            obj => Destroy(obj), // Si il n'y a plus de places dans la pool
            false, // Unity doit-il check si l'objet a déjà été pool ou pas (ici non on fait confiance à notre code)
            maxSpawn / 2, // Taille de base 
            maxSpawn); // Taille max
    }

    protected void Start()
    {
        if (canSpawn && isLooping)
        {
            StartCoroutine(SpawnContinously());
        }
    }

    protected IEnumerator SpawnContinously()
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
                if (hasLimitNumber)
                {
                    if (currentEnabled < maxSpawn)
                    {
                        Spawn();
                        currentEnabled++;
                    }
                }
                else
                {
                    Spawn();
                    currentEnabled++;
                }

                //yield return new WaitForSeconds(timeBetweenSpawns);
                IEnumerator enumerator = CoroutineUtils.WaitSecondsSpecial(this, timeBetweenSpawns, () => GameManager.Instance.IsRunning());
                while (enumerator.MoveNext())
                {
                    // On attend que l'enumerator finisse
                    yield return null;
                }
                yield return null;
            }
            
        }
    }

    /// <summary>
    /// Gestion du spawn (pool.Get ou Instantiate)
    /// </summary>
    /// <returns></returns>
    public GameObject Spawn()
    {
        return pool.Get();
    }

    public static bool IsInTheZone(Vector2 pos)
    {
        return (pos.x >= xMin && pos.x <= xMax && pos.y >= yMin && pos.y <= yMax);
    }

    public static bool IsInTheZoneShape(Vector2 pos, float halfWidth, float halfHeight)
    {
        Debug.Log("taille sprite : " + halfWidth + " " + halfHeight);

        return (pos.x >= xMin + halfWidth && pos.x <= xMax - halfWidth 
            && pos.y >= yMin + halfHeight && pos.y <= yMax - halfHeight);
    }

    /// <summary>
    /// Méthode à appeler pour détruire l'objet et mettre à jour le jeu
    /// </summary>
    /// <param name="obj">Objet à détruire</param>
    public void Kill(GameObject obj)
    {
        ReleaseObject(obj);
        ScoreManager.Instance.AddToScore(GetPoints());
        currentEnabled--;
    }

    /// <summary>
    /// Méthode pour récupérer le nombre de points associé (si jamais le calcul est + compliqué c'est mieux)
    /// </summary>
    /// <returns>Le nombre de points</returns>
    protected virtual int GetPoints()
    {
        return pointsToWin;
    }

    /// <summary>
    /// Méthode pour détruire/désactiver l'objet
    /// </summary>
    /// <param name="obj"></param>
    protected void ReleaseObject(GameObject obj)
    {
        pool.Release(obj);
    }
}
