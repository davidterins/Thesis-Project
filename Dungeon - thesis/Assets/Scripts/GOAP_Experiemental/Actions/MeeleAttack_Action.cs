using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleAttack_Action : Action_Goap
{
  public MeeleAttack_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.MeeleAttackAction;

    PreConditions = new WorldState[]
    {
      WorldState.MeleeEquiped,
      WorldState.AtTarget
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

