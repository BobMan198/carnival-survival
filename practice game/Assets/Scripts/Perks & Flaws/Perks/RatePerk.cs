using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatePerk : Perk
{
    public override void Initialize()
    {
        Debug.Log("Rate Perk Chose!");
        GetComponent<PlayerController>().currentGun.itemRate += 20;
    }
}
