using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject game;
    private GameState gameState = GameState.Running;

    public enum GameState
    {
        Running,
        Paused,
        Ended
    }

    private void Awake()
    {
        Instance = this;
    }

    public void EndGame()
    {
        StartCoroutine(End());
    }

    public void SetPause()
    {
        gameState = GameState.Paused;
    }

    public bool IsPaused()
    {
        return gameState.Equals(GameState.Paused);
    }

    public void SetGameOn()
    {
        gameState = GameState.Running;
    }

    public bool IsRunning()
    {
        return gameState.Equals(GameState.Running);
    }

    public bool IsEnded()
    {
        return gameState.Equals(GameState.Ended);
    }

    private IEnumerator End()
    {
        if (gameState != GameState.Ended)
        {
            gameState = GameState.Ended;
            yield return new WaitForSeconds(1f);

            MethodsDO.SpawnScale(endScreen.transform, 0.5f);

            yield return new WaitForSeconds(0.5f);

            game.SetActive(false);
            Button resetButton = endScreen.GetComponentInChildren<Button>();
            resetButton.Select();
            MethodsDO.ZoomInAndOut(resetButton.transform, 0.8f, 1.2f, 2f);
        }  
    }
}
