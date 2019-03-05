using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal_Goap
{
  protected Planner_Goap planner;
  protected float relevancy;

  public WorldState GoalState;

  public WorldStateSet GoalWorldstates = new WorldStateSet();

  Dictionary<WorldState, bool> currentState;

  protected Goal_Goap(Planner_Goap planner)
  {
    this.planner = planner;
  }

  public virtual bool IsSatisfied(WorldStateSet worldState)
  {
    if (worldState[GoalState])
      return true;
    return false;
  }


  // Requests the planner to create a plan to satisfy this goal
  public Stack<ActionID> TryGetPlan(WorldStateSet currentState, List<Action_Goap> actions)
  {
    var nodePlan = planner.FindPath(currentState, this, actions);
    Stack<ActionID> actionPlan = new Stack<ActionID>(nodePlan.Count);

    foreach (Node_Goap node in nodePlan)
      actionPlan.Push(node.ID);

    return actionPlan;
    
  }

  public static bool operator ==(Goal_Goap lhs, Goal_Goap rhs)
  {
    if (lhs.GoalWorldstates == rhs.GoalWorldstates)
      return true;
    return false;
  }

  public static bool operator !=(Goal_Goap lhs, Goal_Goap rhs)
  {
    if(lhs.GoalWorldstates != rhs.GoalWorldstates)
      return true;
    return false;
  }

  // Determine its relevancy based on the players surroundings & memory
  // returns a value between 0-1
  public virtual float CalculateRelevancy(BlackBoard blackBoard)
  {
    return relevancy;
  }
}
