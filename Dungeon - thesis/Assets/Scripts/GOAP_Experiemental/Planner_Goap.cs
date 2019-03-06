using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planner_Goap
{
  private List<Node_Goap> finalPath;
  Dictionary<ActionID, Node_Goap> totalNodes;


  //TODO put in controller initialization
  public void CreateActionGraph(Dictionary<ActionID, Action_Goap> actions)
  {
    totalNodes = new Dictionary<ActionID, Node_Goap>(actions.Count);

    foreach (Action_Goap action in actions.Values)
    {
      Node_Goap node = new Node_Goap(action.ID, action.Effects)
      {
        NeighBourIDs = new ActionID[action.PreConditions.Length]
      };

      int effectIndex = 0;
      foreach (WorldState preCondition in action.PreConditions)
      {
        foreach (ActionID actionID in WorldStateActionLookup.Table[preCondition])
        {
          node.NeighBourIDs[effectIndex++] = actionID;
        }
      }

      totalNodes.Add(action.ID, node);
    }
  }


  public List<Node_Goap> FindPath(WorldStateSet currentState, Goal_Goap goal, List<Action_Goap> agentActions)
  {
    //ActionID[] startActions = WorldStateActionLookup.Table[goal.GoalState];
    //foreach (ActionID actionID in startActions)
    //{
    //  totalNodes[actionID].CalculateHCost(currentState);
    //  openSet.Add(totalNodes[actionID]);
    //}

    List<Node_Goap> openSet = new List<Node_Goap>();

    HashSet<Node_Goap> closedSet = new HashSet<Node_Goap>();

    Node_Goap startNode = new Node_Goap(currentState, 0, CalculateHCost(currentState, goal), null, ActionID.None);

    openSet.Add(startNode);

    while (openSet.Count > 0)
    {
      Node_Goap currentNode = openSet[0];

      for (int i = 1; i < openSet.Count; i++)
      {
        if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
        {
          currentNode = openSet[i];
        }
      }

      openSet.Remove(currentNode);
      closedSet.Add(currentNode);

     ///Debug.Log("Added " + currentNode.ID + " to closed set");

      if (goal.IsSatisfied(currentNode.worldStates))
      {
        List<Node_Goap> path = new List<Node_Goap>();

        Node_Goap end = currentNode;

        while (end.parent != null)
        {
          path.Add(end);
          end = end.parent;

        }
        return path;
      }

      //foreach (Action_Goap action in agentActions)
      //{
      //  if (action.IsValidInWorldState(currentNode.worldStates))
      //  {
      //    Debug.Log("Action: " + action.ID + " is a valid action in current WorldState");
      //  }
      //}

      //foreach (var keyval in currentNode.worldStates)
      //{
      //  Debug.Log(keyval.Key + " = " + keyval.Value);
      //}

      //Each node reachable from current state.
      foreach (Action_Goap action in agentActions)
      {
        if (action.IsValidInWorldState(currentNode.worldStates))
        {
          //Debug.Log("CURRENT NODE = " + currentNode.ID);

          WorldStateSet effectedWorldState = action.ApplyEffects(currentNode.worldStates);
          Node_Goap potentialNode = new Node_Goap(effectedWorldState, action.ID);

          if (closedSet.Contains(potentialNode))
          {
            continue;
          }

         /// Debug.Log("Checking out Action: " + action.ID);
          //Debug.Log("Effected WorldState");
          //foreach (var keyval in effectedWorldState)
          //{
          //  Debug.Log(keyval.Key + " = " + keyval.Value);
          //}

          if (!openSet.Contains(potentialNode)) //The node is not a member, just add it.
          {
            potentialNode.parent = currentNode;
            potentialNode.gCost = currentNode.gCost + action.cost + CalculateHCost(effectedWorldState, goal);
            openSet.Add(potentialNode);
           /// Debug.Log(action.ID + " was not visited before added to open set.");
          }
          else
          {
            if (currentNode.gCost + action.cost < potentialNode.gCost)
            {
              int index = openSet.IndexOf(potentialNode);
              openSet[index].parent = currentNode;
              openSet[index].gCost = currentNode.gCost + action.cost;
              openSet[index].hCost = CalculateHCost(effectedWorldState, goal);
              openSet[index].ID = action.ID;
             /// Debug.Log("There was a cheaper path to: " + action.ID + " updating its values");
            }
          }
        }
      }

    }
    return null;
  }

  private float CalculateHCost(WorldStateSet current, Goal_Goap target)
  {
    float cost = 0;
    foreach (var stateSet in target.GoalWorldstates)
    {
      if (current[stateSet.Key] != stateSet.Value)
        cost += 1;
    }
    return cost;
  }
}
