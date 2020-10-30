using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagFlaw : Perk
{
    public override void Initialize()
    {
        Debug.Log("Mag Flaw Chose!");
        GetComponent<PlayerController>().currentGun.itemMag -= 8;
    }
}
