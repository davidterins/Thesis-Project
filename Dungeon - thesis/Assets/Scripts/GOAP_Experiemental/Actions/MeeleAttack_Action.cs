using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleAttack_Action : Action_Goap
{
  public MeeleAttack_Action(Action action) : base(action)
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
}

