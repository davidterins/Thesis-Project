using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{

  [SerializeField]
  public readonly int value = 1;

  public override ItemType Type
  {
    get
    {
      return ItemType.Coin; 
    }
  }
}
