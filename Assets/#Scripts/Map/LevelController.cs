using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private PowerUpButtons powerUpButtons;
    private int xpCollected = 0;
    [SerializeField] private int xpToPass = 20;
    [SerializeField] private float xpFactor = 1.2f;


    private void Start()
    {
        Debug.Log("On gère les lv", this);
    }

    private void AddLevel(int gain = 1)
    {
        level+= gain;
        Debug.Log("test set active");
        powerUpButtons.transform.parent.gameObject.SetActive(true);
        powerUpButtons.PauseGame();
    }

    public int GetLevel()
    {
        return level;
    }

    public void CheckLevelGained(int score, int gained)
    {
        xpCollected += gained;
        if (xpCollected%xpToPass == 0)
        {
            AddLevel();
            xpCollected = 0;
            xpToPass = (int) Mathf.Round(xpToPass * xpFactor);
        }
    }
}
