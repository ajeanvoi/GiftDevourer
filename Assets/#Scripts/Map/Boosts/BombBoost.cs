using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BombBoost : AbstractBoost
{
    private PlayerController player;
    [SerializeField] private int nbAddBomb = 2;

    public override void ApplyPower()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        player.AddBomb(nbAddBomb);
        player.UpdateTextBomb();
    }
}
