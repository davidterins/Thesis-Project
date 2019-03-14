using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Goal : Goal_Goap
{
  public Heal_Goal(GameObject owner, Planner_Goap planner) : base(owner, planner)
  {
    GoalWorldstates.Add(WorldStateSymbol.IsHealthy, true);
  }

  //Calculate relevancy to loot something.
  public override float CalculateRelevancy(BlackBoard blackBoard)
  {
    return relevancy;
  }
}
