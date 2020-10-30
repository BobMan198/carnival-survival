using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeFlaw : Perk
{
    public override void Initialize()
    {
        Debug.Log("Range Flaw Chose!");
        GetComponent<PlayerController>().currentGun.itemRange -= 25;
    }
}
