using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTo_Action : Action_Goap
{
  GameObject owner;
  public GoTo_Action(GameObject owner, Action action) : base(action)
  {
    this.owner = owner;
    ID = ActionID.GotoAction;

    PreConditions = new WorldState[0];

    Effects = new WorldState[]
    {
     WorldState.AtTarget,
     WorldState.TargetInRange,
     };
  }

  public override void ExecuteAction()
  {
    //var movement = owner.GetComponent<PlayerMovement>();

    //movement.MoveToTarget();

    base.ExecuteAction();
  }
}
