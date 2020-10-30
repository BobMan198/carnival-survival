using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangePerk : Perk
{
    public override void Initialize()
    {
        Debug.Log("Range Perk Chose!");
        GetComponent<PlayerController>().currentGun.itemRange += 25;
    }
}
