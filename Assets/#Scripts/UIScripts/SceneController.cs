using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void LoadScene(string nameScene)
    {
        DOTween.KillAll();
        SceneManager.LoadScene(nameScene);
    }

    public void Reload()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        LoadScene("Menu");
    }

    private void OnApplicationQuit()
    {
        DOTween.KillAll();
    }
}
