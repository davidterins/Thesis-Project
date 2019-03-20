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
  }

  public override float GetDropRate()
  {
    return 0.4f;
  }

  public override float GetImportance()
  {
    return 1;
  }


}
