﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack_Action : MovingAction_Goap
{
  GameObject attackTarget;


  public RangedAttack_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.RangedAttack;

    PreConditions = new WorldStateSymbol[]
    {
      WorldStateSymbol.RangedEquiped,
     };

    Effects = new WorldStateSymbol[]
    {
     WorldStateSymbol.EnemyDead,
     WorldStateSymbol.LootableItem
     };
  }

  public override void Enter()
  {
    attackTarget = owner.GetComponent<BlackBoard>().EnemyObject;
    if (!attackTarget)
    {
      Failed();
    }
    else
    {
      target = attackTarget.transform.position;
      interactionRange = 2;
      base.Enter();
    }
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

  public override bool IsInRange()
  {
    InRange = Vector2.Distance(owner.transform.position, attackTarget.transform.position) <= interactionRange + 0.5f;
    return InRange;
  }

  public override float GetCost()
  {
    attackTarget = owner.GetComponent<BlackBoard>().EnemyObject;
    if (attackTarget)
      if (Vector2.Distance(owner.transform.position, attackTarget.transform.position) <= interactionRange + 0.5f)
      {
        cost = 2;
      }
      else
        cost = 1;
    return base.GetCost();
  }
}
