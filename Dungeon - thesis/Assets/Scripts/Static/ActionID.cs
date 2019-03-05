using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionID
{
  None,
  GotoAction,
  PickupAction,
  MeeleAttackAction,
  RangedAttackAction,
  ChangeWeapon,
}

public enum ActionCallback
{
  Successfull,
  Failed,
  RunAgain,
}
