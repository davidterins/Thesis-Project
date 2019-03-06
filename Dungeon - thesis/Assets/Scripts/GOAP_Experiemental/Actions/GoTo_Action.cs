using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Go to action.
/// TODO Skulle behöva en targetManager
/// </summary>
public class GoTo_Action : Action_Goap
{
  private readonly GameObject owner;
  Movement movement;

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

  public override void Enter()
  {
    base.Enter();
    movement = owner.GetComponent<Movement>();
    movement.MoveToTarget();
  }

  public override void Execute()
  {

    if(!movement.HasTarget)
    {
      Successfull();
    }

    base.Execute();
  }
}
