using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorldStateSymbol
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
  public static Dictionary<WorldStateSymbol, ActionID[]> Table = new Dictionary<WorldStateSymbol, ActionID[]>()
  {
    { WorldStateSymbol.HasItem, new ActionID[]{ActionID.PickupAction} },
    { WorldStateSymbol.AtTarget, new ActionID[]{ActionID.GotoAction} },
    { WorldStateSymbol.TargetInRange, new ActionID[]{ActionID.GotoAction} },
    { WorldStateSymbol.EnemyDead, new ActionID[]{ActionID.MeeleAttackAction, ActionID.RangedAttackAction} },
    { WorldStateSymbol.RangedEquiped, new ActionID[]{ActionID.ChangeWeapon} },
    { WorldStateSymbol.MeleeEquiped, new ActionID[]{ActionID.ChangeWeapon} },
    { WorldStateSymbol.Win,new ActionID[0] },
  };
}
