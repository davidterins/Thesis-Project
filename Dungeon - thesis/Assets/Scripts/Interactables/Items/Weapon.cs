using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { None, Melee, Range }

public class Weapon : Item
{
  public WeaponType Type;
  public float Range = 0.5f;
  public float Damage = 1;

  protected override WorldStateSymbol GetItemWSEffector()
  {
    switch (Type)
    {
      case WeaponType.Melee:
        return WorldStateSymbol.HasMeleeWeapon;
      
      //case WeaponType.Range:
        //return WorldStateSymbol.HasRangedWeapon;
    }
    return WorldStateSymbol.HasMeleeWeapon;
  }

}
