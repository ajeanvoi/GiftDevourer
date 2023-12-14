using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_SpeedUp : AbstractPowerUp
{
    protected override void ApplyPower()
    {
        PlayerController player = target.gameObject.GetComponent<PlayerController>();
        player.TemporaryBoost(powerTime);
    }
}
