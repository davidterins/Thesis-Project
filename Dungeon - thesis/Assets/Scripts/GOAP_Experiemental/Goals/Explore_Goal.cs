using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explore_Goal : Goal_Goap
{
  private static float baseValue = 0.05f;
  private static float factor = 0.1f;

  public Explore_Goal(GameObject owner, Planner_Goap planner) : base(owner, planner)
  {
    GoalWorldstates.Add(WorldStateSymbol.RoomExplored, true);
  }

  /// <summary>
  /// Calculates the relevancy to explore the dungeon.
  /// </summary>
  /// <param name="blackBoard">The blackboard</param>
  /// <returns></returns>
  public override float CalculateRelevancy(BlackBoard blackBoard)
  {

    float unexploredPercentage = Dungeon.Singleton.CurrentRoom.RoomGraph.UnexploredPercentage;

    // TODO: Right now, the will to explore is always constant depending on personality. 
    //       This could be dynamic in the future depending on how much the agent has explored or memorized.
    relevancy = baseValue + (persona.personalityModifer[Personality.EXPLORATION] * factor) + unexploredPercentage;
    relevancy = Mathf.Clamp(relevancy, 0f, 1f);
    return relevancy;
  }
}
