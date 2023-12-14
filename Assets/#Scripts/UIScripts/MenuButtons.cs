using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    private void Start()
    {
        GetComponentInChildren<Button>().Select();
    }
}
