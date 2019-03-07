using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Goap_Controller : MonoBehaviour
{

  Dictionary<ActionID, Action_Goap> playerActionLookup = new Dictionary<ActionID, Action_Goap>();
  Stack<ActionID> plan;

  List<Action_Goap> playerActions;
  List<Goal_Goap> playerGoals;

  Goal_Goap currentGoal;
  Action_Goap currentAction;

  BlackBoard blackBoard;
  Planner_Goap planner;

  // Dessa ska nog flyttas till blackboarden sen
  WorldStateSet playerWorldState = new WorldStateSet()
  {
    {WorldState.Win, false},
    {WorldState.HasItem, false},
    {WorldState.AtTarget, false},
    {WorldState.TargetInRange, false},
    {WorldState.EnemyDead, false},
    {WorldState.MeleeEquiped, false},
    {WorldState.RangedEquiped, true},
  };


  void Awake()
  {
    blackBoard = GetComponent<BlackBoard>();
    planner = new Planner_Goap();

    playerActions = new List<Action_Goap>{
          new PickupItem_Action(gameObject),
          new GoTo_Action(gameObject),
          new MeeleAttack_Action(gameObject),
          new RangedAttack_Action(gameObject),
          new ChangeWeapon_Action(gameObject),
          new Action_Goap(gameObject)
        };

    playerGoals = new List<Goal_Goap>
    {
      new Loot_Goal(planner),
      new KillEnemy_Goal(planner),
    };

    foreach (Action_Goap action in playerActions)
    {
      playerActionLookup.Add(action.ID, action);
      action.OnActionFinished += Action_ActionCallback;
    }
  }

  /// <summary>
  /// Tänker att denna ska användas efter att en action är klar för att byta
  /// action/ge info om hur det gick att utföra den actionen.
  /// </summary>
  /// <param name="sender">Sender.</param>
  /// <param name="e">E.</param>
  void Action_ActionCallback(object sender, ActionFinishedEventArgs e)
  {
    switch (e.Result)
    {
      case ActionCallback.Successfull:
        if(plan.Count > 0)
        {
          currentAction = playerActionLookup[plan.Pop()];
          currentAction.Enter();
        }
        else
        {
          //currentGoal = GetNewGoal();
          //plan = currentGoal.TryGetPlan(playerWorldState, playerActions);
          //currentAction = playerActionLookup[plan.Pop()];
          //currentAction.Enter();
        }
        break;
      case ActionCallback.Failed:
        //currentGoal = GetNewGoal();
        //plan = currentGoal.TryGetPlan(playerWorldState, playerActions);
        break;
      case ActionCallback.RunAgain:
        break;
      default:
        break;
    }
  }

  //För testningkörning
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.P))
    {
      currentGoal = GetNewGoal();
      plan = currentGoal.TryGetPlan(playerWorldState, playerActions);

      var viewControl = GameObject.FindWithTag("GoapViewController").GetComponent<GoapViewController>();
      viewControl.SetGoal(currentGoal);
      viewControl.SetPlan(plan);

      currentAction = playerActionLookup[plan.Pop()];
      currentAction.Enter();

    }
    if (currentAction != null)
      currentAction.Execute();
  }

  /// <summary>
  /// TODO Gör så att målet räknas ut från goals relevans funktion när minnet är
  /// klart.
  /// Att använda när agenten behöver ett nytt mål.
  /// </summary>
  /// <returns>The new goal.</returns>
  public Goal_Goap GetNewGoal()
  {
    Goal_Goap relevantGoal = null;
    float highestRelevance = -1;
    foreach (Goal_Goap goal in playerGoals)
    {
      float relevance = goal.CalculateRelevancy(blackBoard);

      if (relevance > highestRelevance)
      {
        highestRelevance = relevance;
        relevantGoal = goal;
      }
    }
    Debug.Log("Goal with with highest relevance: " + relevantGoal.GetType());
    return playerGoals[1];// relevantGoal;
  }

  /// <summary>
  /// signa av från actionCallbacken
  /// </summary>
  private void OnDestroy()
  {
    foreach (var action in playerActions)
    {
      action.OnActionFinished -= Action_ActionCallback;
    }
  }

}
