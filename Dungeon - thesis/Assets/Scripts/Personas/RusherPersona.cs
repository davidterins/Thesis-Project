using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

/// <summary>
/// Rusher persona.
/// 0 är dåligt, 1 är bra.
/// </summary>
public class RusherPersona : Persona
{
  private float forcedCombat = 1, hpLeft = 1, forcedLoot = 1;
  private float forcedCombatWeight;
  private float forcedTreasureWeight;

  protected override void Awake()
  {
    base.Awake();

    personalityModifer[Personality.BRAVERY] = 0.4f;
    personalityModifer[Personality.BLOODLUST] = 0.4f;
    personalityModifer[Personality.GREED] = 0.5f;
    personalityModifer[Personality.EXPLORATION] = 0f;
    personalityModifer[Personality.PROGRESSION] = 0.9f;

    forcedCombatWeight = 4f / Dungeon.Singleton.CurrentRoom.RoomGraph.TotalTraversableTiles;
    forcedTreasureWeight = 3f / Dungeon.Singleton.CurrentRoom.RoomGraph.TotalTraversableTiles;
  }

  protected override float CalculateFinalOpinion()
  {
    forcedCombat = Mathf.Clamp(forcedCombat, 0, 1);
    forcedLoot = Mathf.Clamp(forcedLoot, 0, 1);
    hpLeft = Mathf.Clamp(1 - (GetComponent<Player>().Health / HpEnteringRoom), 0, 1);

    return (forcedCombat + hpLeft + forcedLoot) / 3;
  }

  protected override void ResetValues()
  {
    forcedCombat = 1;
    hpLeft = 1;
    forcedLoot = 1;
    HpEnteringRoom = GetComponent<Player>().Health;
  }

  protected override void HandleOnTreasureLoot()
  {
    forcedLoot -= forcedTreasureWeight;
  }

  protected override void HandleOnEnemyDeath()
  {
    forcedCombat -= forcedCombatWeight;
  }
}
