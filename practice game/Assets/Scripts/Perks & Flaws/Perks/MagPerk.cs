using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagPerk : Perk
{
    public override void Initialize()
    {
        Debug.Log("Mag Perk Chose!");
        GetComponent<PlayerController>().currentGun.itemMag += 8;
    }
}
