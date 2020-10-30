using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perk : MonoBehaviour
{

  public static PerkType[] perkType = new PerkType[]
    {
     PerkType.HealthPerk,
     PerkType.RegenPerk,
     PerkType.RangePerk,
     PerkType.MagPerk,
     PerkType.RatePerk
    };

  public enum PerkType
  {
    HealthPerk, 
    RegenPerk,
    RangePerk,
    MagPerk,
    RatePerk
  }

  public static FlawType[] flawType = new FlawType[]
    {
     FlawType.HealthFlaw,
     FlawType.RegenFlaw,
     FlawType.RangeFlaw,
     FlawType.MagFlaw,
     FlawType.RateFlaw
    };

  public enum FlawType
  {
    HealthFlaw, 
    RegenFlaw,
    RangeFlaw,
    MagFlaw,
    RateFlaw
  }

    public abstract void Initialize();

}

