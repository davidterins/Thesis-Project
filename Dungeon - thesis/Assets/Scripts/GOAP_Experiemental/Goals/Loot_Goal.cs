using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot_Goal : Goal_Goap
{
  public Loot_Goal(Planner_Goap planner) : base(planner)
  {
    GoalState = WorldState.HasItem;
    GoalWorldstates = new WorldStateSet()
    {
      {WorldState.HasItem, true},
    };
  }


  //Calculate relevancy to loot something.
  public override float CalculateRelevancy(BlackBoard blackBoard)
  {
    if (blackBoard.Coins < 0)
      relevancy += 0.2f;

    return base.CalculateRelevancy(blackBoard);
  }
}
