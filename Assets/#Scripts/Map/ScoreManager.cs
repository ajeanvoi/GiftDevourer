using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private LevelController levelController;

    public static ScoreManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void UpdateText()
    {
        tmp.text = "Score : " + score;
        tmp.gameObject.GetComponent<Shaker>().DoShake();
    }

    public void AddToScore(int add)
    {
        score += add;
        levelController.CheckLevelGained(score, add);
        UpdateText();
    }

    public float GetScore()
    {
        return score;
    }
}
