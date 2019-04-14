using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSlayerPersona : Persona
{
  protected override void Awake()
  {
    base.Awake();
    enemyDistanceRange = GetComponent<Vision>().GetSightRange() - 1f;
    personalityModifer[Personality.BRAVERY] = 0.9f;
    personalityModifer[Personality.BLOODLUST] = 0.9f;
    personalityModifer[Personality.GREED] = 0.0f;
    personalityModifer[Personality.EXPLORATION] = 0.0f;
    personalityModifer[Personality.PROGRESSION] = 0.1f;
  }

  protected override void HandleOnTreasureLoot()
  {
    //throw new System.NotImplementedException();
  }

  protected override void HandleOnEnemyDeath()
  {
   //throw new System.NotImplementedException();
  }

  protected override float CalculateFinalOpinion()
  {
    //throw new System.NotImplementedException();
    return -1;
  }

  protected override void ResetValues()
  {
    //throw new System.NotImplementedException();
  }
}

