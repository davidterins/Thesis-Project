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
  Movement movement;

  public GoTo_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.GotoAction;
    cost = 2;

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

    // TODO need a good way to find its current walking target.
    // Using an enemy for now.

    movement = owner.GetComponent<Movement>();
    var s = owner.GetComponent<BlackBoard>().AttackTarget.transform.position;
    movement.MoveToTarget(s);
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
