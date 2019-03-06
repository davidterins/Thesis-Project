using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy_Goal : Goal_Goap
{
  public KillEnemy_Goal(Planner_Goap planner) : base(planner)
  {
    GoalWorldstates.Add(WorldState.EnemyDead, true);
    GoalWorldstates = new WorldStateSet()
    {
      {WorldState.EnemyDead, true},
    };
  }

  //Calculate relevancy to loot something.
  public override float CalculateRelevancy(BlackBoard blackBoard)
  {
    if (blackBoard.Coins < 0)
      relevancy += 0.3f;

    return 1;
    //return base.CalculateRelevancy(blackBoard);
  }


}
