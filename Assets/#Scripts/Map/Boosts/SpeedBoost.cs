using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : AbstractBoost
{
    private PlayerController player;
    [SerializeField] private float amountBoost = 0.5f;


    public override void ApplyPower()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }
        player.BoostSpeed(amountBoost);
    }

}
