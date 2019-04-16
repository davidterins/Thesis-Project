using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleAttack_Action : MovingAction_Goap
{
  GameObject attackTarget;

  public MeeleAttack_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.MeeleAttack;

    PreConditions = new WorldStateSymbol[]
    {
      WorldStateSymbol.MeleeEquiped,
      WorldStateSymbol.AvailableEnemy,

     };

    Effects = new WorldStateSymbol[]
    {
     WorldStateSymbol.EnemyDead,
     WorldStateSymbol.LootableItem

     };

    interactionRange = 1f;
  }

  public override void Enter()
  {
    attackTarget = owner.GetComponent<BlackBoard>().TargetEnemyObject;
    if (!attackTarget)
    {
      Failed();
    }
    else
    {
      target = attackTarget.transform.position;

      base.Enter();
    }
  }

  public override bool IsInRange()
  {
    if (Vector2.Distance(owner.transform.position, attackTarget.transform.position) <= interactionRange)
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
        attackTarget.GetComponent<Enemy>().TakeDamage(
      owner, owner.GetComponent<Agent>().Damage);
        Successfull();
      }
    }
  }

  public override float GetCost()
  {
    cost = 14;
    return base.GetCost();
  }


}

