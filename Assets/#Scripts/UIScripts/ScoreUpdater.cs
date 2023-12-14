using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUpdater : MonoBehaviour
{
    TextMeshProUGUI tmp;

    private void OnEnable()
    {
        if (tmp == null)
        {
            tmp = GetComponent<TextMeshProUGUI>();
        }
        tmp.text = "Congratulations!\nYour score is : <color=blue>" + ScoreManager.Instance.GetScore() + "</color>";
    }
}
