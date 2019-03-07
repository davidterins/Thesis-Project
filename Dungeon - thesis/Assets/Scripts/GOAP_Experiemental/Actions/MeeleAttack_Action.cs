using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleAttack_Action : Action_Goap
{
  GameObject attackTarget;

  public MeeleAttack_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.MeeleAttackAction;
    //cost = 2;

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
    attackTarget = owner.GetComponent<BlackBoard>().AttackTarget;
  }

  public override void Execute()
  {
    //base.Execute();
    if (attackTarget != null)
      attackTarget.GetComponent<Enemy>().TakeDamage(101);
    Successfull();
  }
}

