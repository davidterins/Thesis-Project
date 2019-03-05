using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorldState
{
  AtTarget,
  TargetInRange,
  HasItem,
  Win,
  EnemyDead,
  MeleeEquiped,
  RangedEquiped

}

public static class WorldStateActionLookup
{
  public static Dictionary<WorldState, ActionID[]> Table = new Dictionary<WorldState, ActionID[]>()
  {
    { WorldState.HasItem, new ActionID[]{ActionID.PickupAction} },
    { WorldState.AtTarget, new ActionID[]{ActionID.GotoAction} },
    { WorldState.TargetInRange, new ActionID[]{ActionID.GotoAction} },
    { WorldState.EnemyDead, new ActionID[]{ActionID.MeeleAttackAction, ActionID.RangedAttackAction} },
    { WorldState.RangedEquiped, new ActionID[]{ActionID.ChangeWeapon} },
    { WorldState.MeleeEquiped, new ActionID[]{ActionID.ChangeWeapon} },
    { WorldState.Win,new ActionID[0] },
  };
}
