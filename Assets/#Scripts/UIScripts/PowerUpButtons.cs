using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpButtons : MonoBehaviour
{
    private Button[] buttons;
    private Transform cadre;
    [SerializeField] private Vector3 startPos;

    private void OnEnable()
    {
        buttons ??= GetComponentsInChildren<Button>();

        if (cadre == null)
        {
            cadre = GameObject.Find("Cadre").transform;
        }
        //cadre = startPos;
        //Debug.Log("On replace le cadre");
    }

    public void PauseGame()
    {
        GameManager.Instance.SetPause();
        MethodsDO.SpawnScale(this.transform, 0.5f);
        //buttons[0].gameObject.SetActive(true);
        //buttons[0].Select();

        StartCoroutine(WaitToGiveInput());
    }

    public IEnumerator WaitToGiveInput()
    {
        yield return new WaitForSeconds(0.5f);
        buttons[0].Select();
    }

    public void ContinueGame()
    {
        cadre.localPosition = startPos;
        MethodsDO.Despawn(this.transform, 0.5f);
        GameManager.Instance.SetGameOn();
    }
}
