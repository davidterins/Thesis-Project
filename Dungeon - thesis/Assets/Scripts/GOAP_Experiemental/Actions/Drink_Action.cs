using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Remoting.Services;

public class Drink_Action : Action_Goap
{
  Potion potion;

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


  public override void Enter()
  {
    Item item;
    if (owner.GetComponent<Inventory>().TryGetItem(typeof(Potion), out item))
    {
      potion = (Potion)item;
      base.Enter();
    }
    else
    {
      Failed();
    }
  }

  public override void Execute()
  {
    base.Execute();
    potion.Drink(owner);
    Successfull();
  }
}
