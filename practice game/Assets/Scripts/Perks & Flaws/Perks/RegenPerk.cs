using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenPerk : Perk
{
    public override void Initialize()
    {
        Debug.Log("Regen Perk Chose!");
        GetComponent<PlayerController>().playerRegen += 4;
    }
}
