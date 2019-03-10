using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem_Action : MovingAction_Goap
{
  GameObject targetItem;

  public PickupItem_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.PickupAction;

    PreConditions = new WorldStateSymbol[] { WorldStateSymbol.AtTarget };

    Effects = new WorldStateSymbol[] { WorldStateSymbol.HasItem };
  }

  public override void Enter()
  {
    targetItem = owner.GetComponent<BlackBoard>().TargetObject;

    base.Enter();
  }

  public override void Execute()
  {
    if(InRange)
    {
      base.Execute();
      Successfull();
    }

  }

  public override bool IsInRange()
  {
    InRange = Vector2.Distance(owner.transform.position, targetItem.transform.position) <= 0.5;
    return InRange;
  }
}
