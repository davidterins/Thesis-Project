using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Potion : Item
{
  // Start is called before the first frame update
  [SerializeField]
  public readonly int value = 10;

  public override ItemType Type
  {
    get
    {
     return ItemType.Potion;
    }
  }
}
