using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack_Action : Action_Goap
{
  public RangedAttack_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.RangedAttackAction;

    PreConditions = new WorldState[]
    {
      WorldState.RangedEquiped,
      WorldState.TargetInRange
     };

    Effects = new WorldState[]
    {
     WorldState.EnemyDead
     };
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
