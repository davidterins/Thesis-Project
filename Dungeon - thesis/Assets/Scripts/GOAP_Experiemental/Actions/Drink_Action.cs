using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Remoting.Services;

public class Drink_Action : Action_Goap
{
  public Drink_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.Drink;

    PreConditions = new WorldStateSymbol[]
    {
      WorldStateSymbol.HasPotion,
     };

    Effects = new WorldStateSymbol[]
    {
     WorldStateSymbol.IsHealthy,
     };
  }

  public override void Execute()
  {
    owner.GetComponent<Player>().UseItem("Potion");
    Successfull();
  }
}
