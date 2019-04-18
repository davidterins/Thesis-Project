using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy_Goal : Goal_Goap
{
  public KillEnemy_Goal(GameObject owner, Planner_Goap planner) : base(owner, planner)
  {
    GoalWorldstates.Add(WorldStateSymbol.EnemyDead, true);
  }

  /// <summary>
  /// Calculates the relevancy of attacking something
  /// </summary>
  /// <param name="blackBoard">The blackboard</param>
  /// <returns></returns>
  public override float CalculateRelevancy(BlackBoard blackBoard)
  {
    if (!blackBoard.TargetEnemyObject)
      return 0f;

    //float distanceToEnemyFactor = 0; //Vector2.Distance(blackBoard.TargetEnemyObject.transform.position, owner.transform.position) / 10;
    float currentHealth = blackBoard.Health;
    float healthPercentage = 1 - (float)currentHealth / (float)Agent.maxHealth;

    //relevancy = ((persona.personalityModifer[Personality.BLOODLUST] +
    //persona.personalityModifer[Personality.BRAVERY]) / 1.5f) - healthPercentage;

    relevancy = (persona.personalityModifer[Personality.BLOODLUST]) - healthPercentage;


    relevancy = Mathf.Clamp(relevancy, 0f, 1f);
    return relevancy;
  }
}
