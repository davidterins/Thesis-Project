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

  public WorldStateSymbol[] WSDiff { get; private set; }

  //The worldState at this node
  public WorldStateSet WS { get; private set; }

  public Node_Goap(WorldStateSet ws, WorldStateSymbol[] wsDiff, float gCost, float hCost, Node_Goap parent, ActionID actionID)
  {
    this.parent = parent;
    this.WS = ws;
    this.ID = actionID;
    this.hCost = hCost;
    this.WSDiff = wsDiff;

  }

  public Node_Goap(WorldStateSet ws, float gCost, float hCost, Node_Goap parent, ActionID actionID)
  {
    this.parent = parent;
    this.WS = ws;
    this.ID = actionID;
    this.hCost = hCost;
  }


  public Node_Goap(WorldStateSet ws, WorldStateSymbol[] wsDiff, ActionID actionID)
  {
    this.WS = ws;
    this.ID = actionID;
    this.WSDiff = wsDiff;
  }

  public Node_Goap(WorldStateSet ws, ActionID actionID)
  {
    this.WS = ws;
    this.ID = actionID;
  }


  public void AddNeighBour(Node_Goap node)
  {
    
  }

  public bool IsValidInWorldState(WorldStateSet worldState)
  {
    foreach (WorldStateSymbol precondition in WSDiff)
    {
      if (!worldState[precondition])
        return false;
    }
    return true;
  }

  public float CalculateHCost(WorldStateSet currentState)
  {
    float cost = 0;
    foreach (WorldStateSymbol goalState in WSDiff)
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
