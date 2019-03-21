using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Goal : Goal_Goap {
    private float healthScale;

    public Heal_Goal(GameObject owner, Planner_Goap planner) : base(owner, planner) {
        GoalWorldstates.Add(WorldStateSymbol.IsHealthy, true);
        healthScale = owner.GetComponent<Player>().MaxHealth;
    }

    /// <summary>
    /// Calculate relevancy to heal.
    /// </summary>
    /// <param name="blackBoard">The blackboard</param>
    /// <returns></returns>
    public override float CalculateRelevancy(BlackBoard blackBoard) {
    // TODO: Also, add the amount of health pots the agent has divided by 10 to the below formula.
    //       A lot of potions should allow agent to be less conservative.
    relevancy = (owner.GetComponent<Player>().MaxHealth - blackBoard.Health) / healthScale;
    if (relevancy == 1)
      relevancy = 0;
    relevancy = Mathf.Clamp(relevancy, 0f, 1f);
    return relevancy;
  }
}
