using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureHunterPersona : Persona
{

  protected override void Start()
  {
    base.Start();
  
    personalityModifer[Personality.BRAVERY] = 0.2f;
    personalityModifer[Personality.BLOODLUST] = 0.15f;
    personalityModifer[Personality.GREED] = 0.9f;
    personalityModifer[Personality.EXPLORATION] = 0.0f;
    personalityModifer[Personality.PROGRESSION] = 0.1f;
  }

  protected override float CalculateFinalOpinion()
  {
    //throw new System.NotImplementedException();
    return -1;
  }

  protected override void HandleOnEnemyDeath()
  {
    //throw new System.NotImplementedException();
  }

  protected override void HandleOnTreasureLoot()
  {
    //throw new System.NotImplementedException();
  }

  protected override void PrepareForNewRoom(RoomCardModel newCard)
  {
    base.PrepareForNewRoom(newCard);

  }
}
