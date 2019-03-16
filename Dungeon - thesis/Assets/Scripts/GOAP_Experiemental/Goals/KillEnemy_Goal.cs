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
    if (!blackBoard.EnemyObject)
      return 0f;
    relevancy = persona.personalityModifer[Personality.BLOODLUST] +
                   persona.personalityModifer[Personality.BRAVERY] -
                   (Vector2.Distance(blackBoard.EnemyObject.transform.position, owner.transform.position) / 10);

    foreach (GameObject enemy in blackBoard.Memory[TileType.ENEMY])
    {
      if (enemy != null)
      {
        if (enemy != blackBoard.EnemyObject)
        {
          float distance = Vector2.Distance(owner.transform.position, enemy.transform.position);
          if (distance < persona.enemyDistanceRange)
            relevancy -= (1f - (distance / 10) - persona.personalityModifer[Personality.BRAVERY]) / 10;
        }
      }
    }
    return Mathf.Clamp(relevancy, 0f, 1f);
  }
}
