using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal_Goap
{

  protected float relevancy = 0;
  protected Planner_Goap planner;
  protected Persona persona;
  protected GameObject owner;
  public WorldStateSet GoalWorldstates = new WorldStateSet();


  public float Relevancy { get { return relevancy; } }

  protected Goal_Goap(GameObject owner, Planner_Goap planner)
  {
    this.planner = planner;
    this.owner = owner;
    persona = owner.GetComponent<Persona>();
    GoalWorldstates = new WorldStateSet();
  }

  /// <summary>
  /// Checks if this goals world state is satisfied based on the world state it
  /// in parameter
  /// </summary>
  /// <returns><c>true</c>, if satisfied was ised, <c>false</c> otherwise.</returns>
  /// <param name="worldState">World state.</param>
  public virtual bool IsSatisfied(WorldStateSet worldState)
  {
    foreach(var goalState in GoalWorldstates)
    {
      if (worldState[goalState.Key] != goalState.Value)
        return false;
    }
    return true;
  }

 /// <summary>
 /// Tries the get plan.
 /// </summary>
 /// <returns>The get plan.</returns>
 /// <param name="currentState">Agentens current world state.</param>
 /// <param name="actions">Agentens tillgängliga actions.</param>
  public Queue<ActionID> TryGetPlan(WorldStateSet currentState, List<Action_Goap> actions)
  {
    var nodePlan = planner.FindPathFromGoal(currentState, this, actions);
    Queue<ActionID> actionPlan = new Queue<ActionID>(nodePlan.Count);

    foreach (Node_Goap node in nodePlan)
      actionPlan.Enqueue(node.ID);

    return actionPlan;
  }

 /// <summary>
 /// Calculates the relevancy.
 /// Räknar ut hur relevant det här målet är baserat på vad den vet om omvärlden
 /// genom <see cref="BlackBoard"/>
 /// </summary>
 /// <returns>The relevancy.</returns>
 /// <param name="blackBoard">Black board.</param>
  public virtual float CalculateRelevancy(BlackBoard blackBoard)
  {
    return relevancy;
  }

  public WorldStateSet GetEffectedWorldState(WorldStateSet worldState)
  {
    WorldStateSet appliedWorldState = (WorldStateSet)worldState.Clone();

    foreach (WorldStateSymbol effect in GoalWorldstates.Keys)
    {
      appliedWorldState[effect] = true;
    }
    return appliedWorldState;
  }

}
