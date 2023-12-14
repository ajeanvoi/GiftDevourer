using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnableObject
{
    private bool canRun = true;

    public void Continue()
    {
        canRun = true;
    }

    public void Pause()
    {
        canRun = false;
    }

    public bool CanRun()
    {
        return canRun;
    }
}
