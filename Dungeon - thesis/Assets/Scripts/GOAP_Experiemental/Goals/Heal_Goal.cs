using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Goal : Goal_Goap
{
  private float healthScale;

  public Heal_Goal(GameObject owner, Planner_Goap planner) : base(owner, planner)
  {
    GoalWorldstates.Add(WorldStateSymbol.IsHealed, true);
    healthScale = owner.GetComponent<Player>().MaxHealth;
  }

  /// <summary>
  /// Calculate relevancy to heal.
  /// </summary>
  /// <param name="blackBoard">The blackboard</param>
  /// <returns></returns>
  public override float CalculateRelevancy(BlackBoard blackBoard)
  {
    relevancy = 0;
    int currentHealth = blackBoard.Health;
    float healthPercentage = 1 - (float)currentHealth / (float)Agent.maxHealth;

    relevancy = healthPercentage;

    if (owner.GetComponent<Goap_Controller>().PlayerWorldState[WorldStateSymbol.RoomExplored] &&
    blackBoard.TargetEnemyObject == null)
    {

      relevancy = 0;
    }
    //TODO amount of health pots kan kallas via blackboard.checkItemKnowledgeCount.



    if (relevancy == 1)
      relevancy = 0;
    relevancy = Mathf.Clamp(relevancy, 0f, 1f);
    return relevancy;
  }
}
