using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem_Action : MovingAction_Goap
{

  public PickupItem_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.PickupItem;

    PreConditions = new WorldStateSymbol[]
    {
      WorldStateSymbol.LootableItem
    };

    Effects = new WorldStateSymbol[]
    {
     WorldStateSymbol.HasItem
     };
  }

  public override void Enter()
  {
    target = owner.GetComponent<BlackBoard>().LootPosition;
    //if (target)
    //{
    //  Failed();
    //}
    //else
    {
      //target = targetItem.transform.position;
      base.Enter();
    }
  }

  public override void Execute()
  {
    if (InRange)
    {
      base.Execute();
      Successfull();
    }

  }

  public override bool IsInRange()
  {
    InRange = Vector2.Distance(owner.transform.position, target) <= 0.5;
    return InRange;
  }
}
