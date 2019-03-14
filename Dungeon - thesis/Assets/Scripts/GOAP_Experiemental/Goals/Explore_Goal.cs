using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explore_Goal : Goal_Goap
{
  public Explore_Goal(GameObject owner, Planner_Goap planner) : base(owner, planner)
  {
    GoalWorldstates.Add(WorldStateSymbol.RoomExplored, true);
  }

  //Calculate relevancy to loot something.
  public override float CalculateRelevancy(BlackBoard blackBoard)
  {
  
    return relevancy;

  }
}
