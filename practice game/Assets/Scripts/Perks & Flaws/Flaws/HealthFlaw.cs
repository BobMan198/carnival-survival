using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFlaw : Perk
{
    public override void Initialize()
    {
        Debug.Log("Health Flaw Chose!");
        GetComponent<PlayerController>().playerMaxHealth -= 25;
    }
}
