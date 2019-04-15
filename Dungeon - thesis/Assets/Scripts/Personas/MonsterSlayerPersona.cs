using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSlayerPersona : Persona
{
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
  }

  protected override void HandleOnEnemyDeath()
  {
  }

  protected override float CalculateFinalOpinion()
  {
    return -1;
  }

  protected override void PrepareForNewRoom(RoomCardModel newCard)
  {
    base.PrepareForNewRoom(newCard);
  }
}

