using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Monster slayer persona.
/// Opimalt.
/// Döda alla enemies i rummet och överleva.
/// </summary>
public class MonsterSlayerPersona : Persona
{
  private float roomTotalEnemies;
  private float slainEnemies;

  private float actionValue = 0;
  private float combatWeight;

  protected override void Start()
  {
    base.Start();
    personalityModifer[Personality.BLOODLUST] = 0.6f;
    personalityModifer[Personality.GREED] = 0.1f;
    personalityModifer[Personality.PROGRESSION] = 0.12f;

    combatWeight = 30f / Dungeon.Singleton.CurrentRoom.RoomGraph.TotalTraversableTiles;
  }

 
  protected override float CalculateFinalOpinion()
  {

    //if(GetComponent<Player>().Health <= 0)
    //{
    //  return 0;
    //}

    if (slainEnemies <= 0)
    {
      if (OutPutPairs.ContainsKey("Eliminated enemies"))
      {
        OutPutPairs.Remove("Eliminated enemies");
      }
      return 0;
    }

    actionValue = Mathf.Clamp(actionValue, 0, 1);
    OutPutPairs["Action value"] = actionValue;

    //float slainEnemyPercentage = slainEnemies / roomTotalEnemies;
    //OutPutPairs["Eliminated enemies"] = slainEnemyPercentage * 100 + "%";


    return actionValue;
  }

  protected override void HandleOnTreasureLoot()
  {
    //Neutral
    //if (minInteractionsToGetKeys <= 0)
    //{
    //slainEnemies++;
    //actionValue += combatWeight;
    //}
    minInteractionsToGetKeys--;
  }

  protected override void HandleOnEnemyDeath()
  {
    //if (minInteractionsToGetKeys <= 0)
    //{
    slainEnemies++;
    actionValue += combatWeight;
    //}
    minInteractionsToGetKeys--;
  }


  protected override void PrepareForNewRoom(RoomCardModel newCard)
  {
    base.PrepareForNewRoom(newCard);

    roomTotalEnemies = Dungeon.Singleton.CurrentRoom.StartingEnemyCount;
    slainEnemies = 0;
    actionValue = 0;

  }
}

