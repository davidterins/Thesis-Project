using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy_Goal : Goal_Goap {
    public KillEnemy_Goal(Planner_Goap planner) : base(planner) {
        GoalWorldstates.Add(WorldStateSymbol.EnemyDead, true);
    }

    // Calculate relevancy to attack something.
    public override float CalculateRelevancy(BlackBoard blackBoard) {
        relevancy = blackBoard.GetAttackEnemyRelevancy();        
        return relevancy;
        //return base.CalculateRelevancy(blackBoard);
    }


}
