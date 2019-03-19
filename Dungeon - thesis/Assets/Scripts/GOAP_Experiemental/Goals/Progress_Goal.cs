using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress_Goal : Goal_Goap
{
  private static float baseValue = 0.05f;

  public Progress_Goal(GameObject owner, Planner_Goap planner) : base(owner, planner)
  {
    GoalWorldstates.Add(WorldStateSymbol.Progress, true);  
  }

  /// <summary>
  /// Calculates the relevancy to progress, ie find the exit door.
  /// </summary>
  /// <param name="blackBoard">The blackboard</param>
  /// <returns></returns>
  public override float CalculateRelevancy(BlackBoard blackBoard)
  {
    //return 1;
    // TODO: See below pls.
    if(blackBoard.HasKey)
      relevancy = persona.personalityModifer[Personality.PROGRESSION] + baseValue;
    else
      return 0f;
    return Mathf.Clamp(relevancy, 0f, 1f);
  }
}
