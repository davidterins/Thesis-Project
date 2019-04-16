using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Potion : Item
{
  [SerializeField]
  private int value = 25;

  private void Start()
  {
    importance = 1;
  }

  public override float GetDropRate()
  {
    return 1f;
  }

  public override float GetImportance()
  {
    return 1;
  }

  protected override WorldStateSymbol GetItemWSEffector()
  {
    return WorldStateSymbol.HasPotion;
  }

  public void Drink(GameObject player)
  {
    player.GetComponent<Player>().ModifyHealth(value);
    Used();
  }
}
