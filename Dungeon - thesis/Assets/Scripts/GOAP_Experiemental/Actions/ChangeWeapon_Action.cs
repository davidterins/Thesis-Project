using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon_Action : Action_Goap
{
  public ChangeWeapon_Action(Action action) : base(action)
  {
    ID = ActionID.ChangeWeapon;

    PreConditions = new WorldState[0];

    Effects = new WorldState[]
    {
     WorldState.MeleeEquiped,
     WorldState.RangedEquiped,
     };
  }
}
