﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Goap_Controller : MonoBehaviour
{

  Dictionary<ActionID, Action_Goap> playerActionLookup = new Dictionary<ActionID, Action_Goap>();
  Queue<ActionID> plan;

  List<Action_Goap> playerActions;
  List<Goal_Goap> playerGoals;

  Goal_Goap currentGoal;
  Action_Goap currentAction;

  BlackBoard blackBoard;
  Planner_Goap planner;
  Persona persona;


  public WorldStateSet PlayerWorldState { get { return playerWorldState; } }
  WorldStateSet playerWorldState = new WorldStateSet()
  {
    {WorldStateSymbol.HasItem, false},
    {WorldStateSymbol.EnemyDead, false},
    {WorldStateSymbol.SecondaryWeapon, false},
    {WorldStateSymbol.MeleeEquiped, true },
    {WorldStateSymbol.RangedEquiped, false },
    {WorldStateSymbol.LootableItem, false},
    {WorldStateSymbol.AvailableChest, true },
    {WorldStateSymbol.HasPotion, false},
    {WorldStateSymbol.IsHealthy, false},
    {WorldStateSymbol.RoomExplored, true }
  };

  void Awake()
  {
    blackBoard = GetComponent<BlackBoard>();
    persona = GetComponent<Persona>();
    planner = new Planner_Goap();

    playerActions = new List<Action_Goap>{
          new PickupItem_Action(gameObject),
          new MeeleAttack_Action(gameObject),
          new RangedAttack_Action(gameObject),
          new ChangeWeapon_Action(gameObject),
          new OpenChest_Action(gameObject),
          new Drink_Action(gameObject),
          new Action_Goap(gameObject)
        };

    playerGoals = new List<Goal_Goap>
    {
      new Loot_Goal(gameObject, planner),
      new KillEnemy_Goal(gameObject, planner),
      new Heal_Goal(gameObject, planner),
      new Explore_Goal(gameObject, planner)
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
    
    var viewControl = GameObject.FindWithTag("GoapViewController").GetComponent<GoapViewController>();
    viewControl.UpdateActionStatus(e.Result);

    switch (e.Result)
    {
      case ActionCallback.Successfull:
        if (plan.Count > 0)
        {
        
          currentAction = playerActionLookup[plan.Dequeue()];
          currentAction.Enter();
        }
        else
        {
          //CreateNewPlan();
        }
        break;
      case ActionCallback.Failed:
        //currentAction = null playerActionLookup[plan.Dequeue()];
        plan.Clear();
        currentAction = null;
        // CreateNewPlan();
        //currentGoal = GetNewGoal();
        //plan = currentGoal.TryGetPlan(playerWorldState, playerActions);
        break;
    }


  }

  void Update()
  {
    CreatePlanOnP();
  }

  void CreatePlanOnP()
  {
    if (Input.GetKeyDown(KeyCode.P))
    {
      CreateNewPlan();
    }
  }

  private void CreateNewPlan()
  {
    currentGoal = GetNewGoal();
    plan = currentGoal.TryGetPlan(playerWorldState, playerActions);

    var viewControl = GameObject.FindWithTag("GoapViewController").GetComponent<GoapViewController>();
    viewControl.SetGoal(currentGoal);
    viewControl.SetPlan(plan);

    currentAction = playerActionLookup[plan.Dequeue()];
    currentAction.Enter();
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
   //Debug.Log("Goal with with highest relevance: " + relevantGoal.GetType());
    //return playerGoals[2];// relevantGoal;
    return relevantGoal;
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