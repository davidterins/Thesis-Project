using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
  public float Range = 0.5f;

  public override ItemType Type
  {
    get
    {
      return ItemType.Weapon;
    }
  }
}
