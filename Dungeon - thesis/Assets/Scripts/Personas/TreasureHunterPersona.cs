using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Treasure hunter persona.
/// Optimal: All treasures looted, no enemies killed.
/// </summary>
public class TreasureHunterPersona : Persona
{
  private float forcedCombat = 1, hpChange = 1;

  private float roomTotalTreasures;
  private float lootedTreasures;

  private float lootValue = 0;
  private float lootWeight;

  private float forcedCombatWeight;

  protected override void Start()
  {
    base.Start();

    personalityModifer[Personality.BLOODLUST] = 0.23f;
    personalityModifer[Personality.GREED] = 0.9f;
    personalityModifer[Personality.PROGRESSION] = 0.2f;

    forcedCombatWeight = 30f / Dungeon.Singleton.CurrentRoom.RoomGraph.TotalTraversableTiles;
    lootWeight = 30f / Dungeon.Singleton.CurrentRoom.RoomGraph.TotalTraversableTiles;
  }

  protected override float CalculateFinalOpinion()
  {

    //if (lootedTreasures <= 0)
    //{
    //  if (OutPutPairs.ContainsKey("Looted treasures"))
    //  {
    //    OutPutPairs.Remove("Looted treasures");
    //  }
    //  return 0;
    //}

    forcedCombat = Mathf.Clamp(forcedCombat, 0, 1);
    OutPutPairs["Forced combat"] = forcedCombat;

    lootValue = Mathf.Clamp(lootValue, 0, 1);
    OutPutPairs["Loot value"] = lootValue;

    //float lootedTreasurePercentage = lootedTreasures / roomTotalTreasures;
    //OutPutPairs["Looted treasures"] = lootedTreasurePercentage * 100 + "%";

    return (forcedCombat + lootValue) / 2;
  }

  protected override void HandleOnEnemyDeath()
  {
    forcedCombat -= forcedCombatWeight;
  }

  protected override void HandleOnTreasureLoot()
  {
    lootValue += lootWeight;
    //if (minInteractionsToGetKeys <= 0)
    //{
    //  lootedTreasures++;
    //}
    minInteractionsToGetKeys--;
  }

  protected override void PrepareForNewRoom(RoomCardModel newCard)
  {
    base.PrepareForNewRoom(newCard);

    roomTotalTreasures = Dungeon.Singleton.CurrentRoom.StartingTreasureCount;
    forcedCombat = 1;
    lootedTreasures = 0;
    lootValue = 0;
  }
}
