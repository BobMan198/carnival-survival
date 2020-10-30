using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/New Gun")]
public class GunInfo : ItemInfo
{
    public string itemId;
    public float itemDamage;
    public float itemMag;
    public float itemRange;
    public float itemRate;
    public GameObject gunModel;
    public Vector3 itemHolderOffset;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
}
