using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
  public int Coins = 0;

  

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public override void HandlePickup(Item item)
  {
    if (item.GetType() == typeof(Coin))
    {
      Coins += ((Coin)item).value;
    }
    else if (item.GetType() == typeof(Potion))
    {
      Health += ((Potion)item).value;
    }
  }
}
