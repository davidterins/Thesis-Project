using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_Goal : Goal_Goap
{

  public Win_Goal(GameObject owner, Planner_Goap planner) : base(owner, planner)
  {
    GoalWorldstates.Add(WorldStateSymbol.CanWin, true);
  }

  /// <summary>
  /// Calculates the relevancy to explore the dungeon.
  /// </summary>
  /// <param name="blackBoard">The blackboard</param>
  /// <returns></returns>
  public override float CalculateRelevancy(BlackBoard blackBoard)
  {
    // TODO Kanske kolla här hur många fiender som är kvar och hur många procent av rummen
    // är explorade osv och basera relvancen olika per persona.
    if(blackBoard.Memory.ContainsKey(typeof(Portal)))
    {
      relevancy = 0.1f + persona.personalityModifer[Personality.PROGRESSION];
    }
    relevancy = Mathf.Clamp(relevancy, 0f, 1f);
    return relevancy;
  }
}
