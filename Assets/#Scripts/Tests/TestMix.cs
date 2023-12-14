using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMix : MonoBehaviour
{
    public int tabSize = 20;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Use P to test MixTab with int and K with string");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GenerateTestMixInt();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GenerateTestMixString();
        }
    }

    public void GenerateTestMixInt()
    {
        int[] tab = new int[tabSize];
        for (int i = 0; i < tabSize; i++)
        {
            tab[i] = i;
        }
        Debug.Log("Avant mélange : " + GetStringTab(tab));
        tab = MethodsDO.MelangeTab(tab);
        Debug.Log("Après mélange : " + GetStringTab(tab));
    }

    public void GenerateTestMixString()
    {
        string[] tab = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        Debug.Log("Avant mélange : " + GetStringTab(tab));
        tab = MethodsDO.MelangeTab(tab);
        Debug.Log("Après mélange : " + GetStringTab(tab));
    }

    public string GetStringTab<T>(T[] tab)
    {
        string msg = "[";
        foreach (T t in tab)
        {
            msg += t.ToString() + ";";
        }
        msg += "]";

        return msg;
    }
}
