using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon_Action : Action_Goap
{
  public ChangeWeapon_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.ChangeWeapon;

    PreConditions = new WorldStateSymbol[0];

    Effects = new WorldStateSymbol[]
    {
     WorldStateSymbol.MeleeEquiped,
     WorldStateSymbol.RangedEquiped,
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
