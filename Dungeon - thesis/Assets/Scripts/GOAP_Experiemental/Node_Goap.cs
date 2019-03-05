using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_Goap
{
  //ID is used to map to the correct Action_Goap.
  public ActionID ID { get; set; }
  public Node_Goap parent;
  public ActionID[] NeighBourIDs;
  public float gCost = 1;
  // Heuristic cost is the number of symbols different between the current
  // and goal states
  public float hCost;
  public float fCost { get { return gCost + hCost; } }

  WorldState[] goalStates;

  //The worldState at this node
 public WorldStateSet worldStates { get; private set; }

  public Node_Goap(ActionID ID, WorldState[] goalStates)
  {
    this.ID = ID;
    this.goalStates = goalStates;
  }

  public Node_Goap(WorldStateSet worldStates, float gCost, float hCost, Node_Goap parent, ActionID actionID)
  {
    this.parent = parent;
    this.worldStates = worldStates;
    this.ID = actionID;
    this.hCost = hCost;
  }

  public Node_Goap(WorldStateSet worldStates, ActionID actionID)
  {
    this.worldStates = worldStates;
    this.ID = actionID;
  }


  public float CalculateHCost(WorldStateSet currentState)
  {
    float cost = 0;
    foreach (WorldState goalState in goalStates)
    {
      if (currentState[goalState] == false)
      {
      cost += 1;
      }
    }
    hCost = cost;
    return cost;
  }
}
