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
  private float forcedCombat = 1, hpChange = 1, forcedLoot = 1;
  private float forcedCombatWeight;
  private float forcedTreasureWeight;

  protected override void Start()
  {
    base.Start();

    personalityModifer[Personality.BLOODLUST] = 0.53f;
    personalityModifer[Personality.GREED] = 0.5f;
    personalityModifer[Personality.PROGRESSION] = 0.9f;

    forcedCombatWeight = 30f / Dungeon.Singleton.CurrentRoom.RoomGraph.TotalTraversableTiles;
    forcedTreasureWeight = 20f / Dungeon.Singleton.CurrentRoom.RoomGraph.TotalTraversableTiles;
  }

  protected override float CalculateFinalOpinion()
  {
    forcedCombat = Mathf.Clamp(forcedCombat, 0, 1);
    OutPutPairs["ForcedCombat"] = forcedCombat;

    forcedLoot = Mathf.Clamp(forcedLoot, 0, 1);
    OutPutPairs["ForcedLoot"] = forcedLoot;

    float currentHealth = GetComponent<Player>().Health;
    if (currentHealth < HpEnteringRoom)
    {
      hpChange = (1 - (currentHealth / HpEnteringRoom));
      OutPutPairs["HPChange"] = "\n" +
       " Enter: " + HpEnteringRoom + "\n" +
       " Exit: " + currentHealth + "\n" +
       " Change: " + hpChange * 100 + "%";
    }
    else
    {
      if (OutPutPairs.ContainsKey("HPChange"))
      {
        OutPutPairs.Remove("HPChange");
      }
    }

    return (forcedCombat + hpChange + forcedLoot) / 3;
  }

  protected override void PrepareForNewRoom(RoomCardModel newCard)
  {
    base.PrepareForNewRoom(newCard);

    forcedCombat = 1;
    hpChange = 1;
    forcedLoot = 1;
  }

  protected override void HandleOnTreasureLoot()
  {
    if (minInteractionsToGetKeys <= 0)
    {
      forcedLoot -= forcedTreasureWeight;
    }
    minInteractionsToGetKeys--;
  }

  protected override void HandleOnEnemyDeath()
  {
    if (minInteractionsToGetKeys <= 0)
    {
      forcedCombat -= forcedCombatWeight;
    }
    minInteractionsToGetKeys--;
  }
}
