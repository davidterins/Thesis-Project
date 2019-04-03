﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for an action that need to be at target to be able to execute.
/// </summary>
public abstract class MovingAction_Goap : Action_Goap
{
  protected bool InRange;
  protected float interactionRange = 0.001f;
  protected Vector2 target;
  protected Movement movement;

  protected MovingAction_Goap(GameObject owner) : base(owner) { }

  public override void Enter()
  {
    base.Enter();
  }

  protected override bool CanExecute()
  {
    if (base.CanExecute())
    {
      if (!IsInRange())
      {
        movement = owner.GetComponent<Movement>();
        Debug.Log("Target was not in range, walk to target");
        if (movement.TryMoveToTarget(target, interactionRange))
        {
          Debug.LogWarning(ID + "Signed up for AtDestination event");
          movement.AtDestination += HandleAtDestination;
        }
        else
        {
          Debug.Log("No Path to target");
          Failed();
          return false;
        }
      }
    }
    return true;
  }

  void HandleAtDestination(object sender, EventArgs e)
  {
    movement.PrintAtTargetInvocationList();
    movement.AtDestination -= HandleAtDestination;
    Debug.LogWarning(ID + "Removed from AtDestination event");
   
    Enter();
  }

  public abstract bool IsInRange();

  public override void Execute()
  {
    base.Execute();
  }

  protected override void Successfull()
  {

    base.Successfull();
  }

}
