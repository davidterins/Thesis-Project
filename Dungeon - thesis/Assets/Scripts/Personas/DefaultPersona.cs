using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPersona : Persona
{

  protected override void Awake()
  {
    base.Awake();
    personalityModifer[Personality.BRAVERY] = 0.5f;
    personalityModifer[Personality.BLOODLUST] = 0.4f;
    personalityModifer[Personality.GREED] = 0.4f;
    personalityModifer[Personality.EXPLORATION] = 0.3f;
    personalityModifer[Personality.PROGRESSION] = 0.2f;
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

  protected override void ResetValues()
  {
    //throw new System.NotImplementedException();
  }
}
