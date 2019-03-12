using System;
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
    if (!IsInRange())
    {
      movement = owner.GetComponent<Movement>();
      Debug.Log("Target was not in range, walk to target");
      if (movement.TryMoveToTarget(target, interactionRange))
      {
        movement.AtDestination += HandleAtDestination;
      }
      else
      {
        Debug.Log("No Path to target");
        Failed();
      }
    }
    else
    {
      Execute();
    }

  }

  void HandleAtDestination(object sender, EventArgs e)
  {
    movement.AtDestination -= HandleAtDestination;
    Enter();
  }

  public abstract bool IsInRange();

  public override void Execute()
  {
    base.Execute();
  }

}
