using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon_Action : Action_Goap
{
  public ChangeWeapon_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.ChangeWeapon;

    PreConditions = new WorldState[0];

    Effects = new WorldState[]
    {
     WorldState.MeleeEquiped,
     WorldState.RangedEquiped,
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
