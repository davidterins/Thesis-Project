using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem_Action : Action_Goap
{
  public PickupItem_Action(GameObject owner, FSM FSM) : base(owner, FSM)
  {
    ID = ActionID.PickupAction;

    PreConditions = new WorldState[] { WorldState.AtTarget };

    Effects = new WorldState[] { WorldState.HasItem };
   
  }

  public override void Enter()
  {
    base.Enter();
  }

  public override void Execute()
  {
    base.Execute();
    Successfull();
  }
}
