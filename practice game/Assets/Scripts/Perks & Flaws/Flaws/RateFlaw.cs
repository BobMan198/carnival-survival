using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateFlaw : Perk
{
    public override void Initialize()
    {
        Debug.Log("Rate Flaw Chose!");
        GetComponent<PlayerController>().currentGun.itemRate -= 20;
    }
}
