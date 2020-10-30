using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenFlaw : Perk
{
    public override void Initialize()
    {
        Debug.Log("Regen Flaw Chose!");
        GetComponent<PlayerController>().playerRegen -= 4;
    }
}
