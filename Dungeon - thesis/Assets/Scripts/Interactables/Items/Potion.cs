using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Potion : Item
{
  [SerializeField]
  public readonly int value = 25;

  private void Start()
  {
    importance = 1;
    dropRate = 1;
  }

  public override float GetDropRate()
  {
    return 1;
  }


}
