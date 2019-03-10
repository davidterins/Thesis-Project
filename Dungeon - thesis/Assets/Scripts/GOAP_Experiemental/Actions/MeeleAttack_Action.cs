using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleAttack_Action : MovingAction_Goap
{
  GameObject attackTarget;

  public MeeleAttack_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.MeeleAttackAction;
    //cost = 2;

    PreConditions = new WorldState[]
    {
      WorldState.MeleeEquiped,
     };

    Effects = new WorldState[]
    {
     WorldState.EnemyDead
     };
  }

  public override void Enter()
  {
    attackTarget = owner.GetComponent<BlackBoard>().TargetObject;
    if (!attackTarget)
    {
      Failed();
    }
    else
    {
      target = attackTarget.transform.position;
      interactionRange = 1;
      base.Enter();
    }
  }

  public override bool IsInRange()
  {
    if (Vector2.Distance(owner.transform.position, attackTarget.transform.position) <= interactionRange + 0.5)
    {
      return InRange = true;
    }
    return InRange = false;
  }

  public override void Execute()
  {
    if (InRange)
    {
      if (attackTarget != null)
      {
        attackTarget.GetComponent<Enemy>().TakeDamage(101);
        Successfull();
      }
    }
  }


}

