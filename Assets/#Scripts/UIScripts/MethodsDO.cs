using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class MethodsDO
{
    /// <summary>
    /// Méthode à utiliser pour faire spawn des panels déjà existants (par exemple)
    /// </summary>
    /// <param name="tr">Le Transform de l'objet en question</param>
    /// <param name="duration">La durée de l'animation</param>
    public static void SpawnScale(Transform tr, float duration)
    {
        tr.gameObject.SetActive(true);
        tr.localScale = Vector3.zero;
        tr.DOScale(Vector3.one, duration).SetEase(Ease.Flash);
    }

    /// <summary>
    /// Méthode pour faire "despawn" dans le sens désactivé
    /// </summary>
    /// <param name="tr">Le Transform de l'objet en question</param>
    /// <param name="duration">La durée de l'animation</param>
    public static void Despawn(Transform tr, float duration)
    {
        tr.localScale = Vector3.one;
        tr.DOScale(Vector3.zero, duration).SetEase(Ease.Flash);
        tr.gameObject.SetActive(false);
    }

    /// <summary>
    /// Méthode pour avoir un effet de zoom+/- sur un objet via son scale
    /// </summary>
    /// <param name="tr">Le Transform de l'objet en question</param>
    /// <param name="scaleMax">Facteur d'échelle maximal</param>
    /// <param name="scaleMin">Facteur d'échelle minimal</param>
    /// <param name="duration">durée de l'animation (d'une boucle max-min, min-max)</param>
    public static void ZoomInAndOut(Transform tr, float scaleMax, float scaleMin, float duration)
    {
        tr.localScale = scaleMin * Vector3.one;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(tr.DOScale(scaleMax, duration/2));
        sequence.Append(tr.DOScale(scaleMin, duration/2));

        sequence.SetLoops(-1, LoopType.Restart);
        sequence.Play();
    }


    /// <summary>
    /// Méthode pour obtenir le point de spawn d'un objet en se basant sur les coins de la map
    /// </summary>
    /// <param name="target">La position de l'objet qu'il faut éviter lors du spawn</param>
    /// <param name="nbParties">Le nombre de parties utilisées pour découper la map (1 si aucune découpe)</param>
    /// <returns></returns>
    public static Vector3 ChooseSpawnPosition(Transform target, int nbParties)
    {
        //// On découpe la map en nbParties tranches verticales
        //Vector3[] corners = UniversalSpawner.corners;

        //float width = (corners[3].x - corners[0].x) / nbParties; // longueur d'une partie
        //float height = corners[2].y - corners[0].y; // hauteur d'une partie

        //Rect[] parties = new Rect[nbParties];
        //int j = 0;
        //float currentX = corners[0].x;
        //float currentY = corners[0].y;
        //while (j < nbParties)
        //{
        //    parties[j] = new Rect(currentX, currentY, width, height);
        //    // Debug.Log(parties[j]);
        //    currentX += width;
        //    j++;
        //}

        //// On regarde où est le joueur et on fait spawn aléatoirement le policier dans une tranche à 2 de distances
        //int nbRectPlayer = 0; // sur quelle partie est le joueur

        //for (int i = 0; i < parties.Length; i++)
        //{
        //    if (parties[i].Contains(target.position))
        //    {
        //        nbRectPlayer = i;
        //        break;
        //    }
        //}

        //// int areaToSpawn = (nbRectPlayer + 2) % nbParties;
        //int areaToSpawn = Random.Range(0, nbParties - 1);
        //if (areaToSpawn == nbRectPlayer)
        //{
        //    areaToSpawn = nbParties - 1;
        //}
        //Rect rectToSpawn = parties[areaToSpawn];

        //float randomX = Random.Range(rectToSpawn.x, rectToSpawn.x + width);
        //float randomY = Random.Range(rectToSpawn.y, rectToSpawn.y + height);

        //return new Vector3(randomX, randomY, 0);

        Vector3 targetPos = target.transform.position;
        Vector3 posEnemy = target.transform.position;

        float distanceMin = 2f;

        Vector3[] corners = UniversalSpawner.corners;

        while (Vector2.Distance(targetPos, posEnemy) < distanceMin){
            float randomX = Random.Range(corners[0].x, corners[3].x);
            float randomY = Random.Range(corners[0].y, corners[2].y);

            posEnemy = new Vector2(randomX, randomY);
        }
        // Debug.Log(posEnemy);
        return posEnemy;

    }

    public static T[] MelangeTab<T>(T[] oldTab)
    {
        int size = oldTab.Length;

        (float, int)[] nl = new (float, int)[size]; // Tab avec la valeur rd et l'index

        // On crée le tableau de permutation
        for (int i = 0; i < size; i++)
        {
            nl[i]= (Random.Range(0f, 1f), i);
        }

        // On trie le tableau /t au 1er élément du doublet
        System.Array.Sort(nl, (x, y) => x.Item1.CompareTo(y.Item1));

        T[] tab = new T[size];
        System.Array.Copy(oldTab, tab, size);

        int index = 0;

        foreach ((float, int) doublet in nl)
        {
            tab[index] = oldTab[nl[index].Item2];
            index++;
        }

        return tab;
    }    
}
