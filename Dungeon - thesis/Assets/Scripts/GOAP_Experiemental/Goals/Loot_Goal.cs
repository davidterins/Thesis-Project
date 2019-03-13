using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot_Goal : Goal_Goap {
    public Loot_Goal(Planner_Goap planner) : base(planner) {
        GoalWorldstates.Add(WorldStateSymbol.HasItem, true);
    }

    //Calculate relevancy to loot something.
    public override float CalculateRelevancy(BlackBoard blackBoard) {
        //relevancy = blackBoard.GetLootTreasureRelevancy();
        return relevancy;
        //if (blackBoard.Coins < 0)
        //    relevancy += 0.2f;

        //return base.CalculateRelevancy(blackBoard);
    }
}
