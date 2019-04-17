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

  protected override void Start()
  {
    base.Start();
    personalityModifer[Personality.BRAVERY] = 0.9f;
    personalityModifer[Personality.BLOODLUST] = 0.9f;
    personalityModifer[Personality.GREED] = 0.0f;
    personalityModifer[Personality.EXPLORATION] = 0.0f;
    personalityModifer[Personality.PROGRESSION] = 0.1f;
  }

  protected override void HandleOnTreasureLoot()
  {
    //Neutral
  }

  protected override void HandleOnEnemyDeath()
  {
    slainEnemies++;
  }

  protected override float CalculateFinalOpinion()
  {
    if (slainEnemies <= 0)
    {
      if(OutPutPairs.ContainsKey("Eliminated enemies"))
      {
        OutPutPairs.Remove("Eliminated enemies");
      }
      return 0;
    }

    float slainEnemyPercentage = slainEnemies / roomTotalEnemies;
    OutPutPairs["Eliminated enemies"] = slainEnemyPercentage * 100 + "%";


    return slainEnemyPercentage;
  }

  protected override void PrepareForNewRoom(RoomCardModel newCard)
  {
    base.PrepareForNewRoom(newCard);

    roomTotalEnemies = Dungeon.Singleton.CurrentRoom.StartingEnemyCount;
    slainEnemies = 0;

  }
}

