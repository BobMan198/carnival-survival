using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPerk : Perk
{
    public override void Initialize()
    {
        Debug.Log("Health Perk Chose!");
        GetComponent<PlayerController>().playerMaxHealth += 25;
    }
}
