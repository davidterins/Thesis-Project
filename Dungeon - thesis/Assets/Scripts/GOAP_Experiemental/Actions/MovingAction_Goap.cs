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
  protected float distanceFromTarget;
  bool signedUpOnMovement;

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

          Debug.LogWarning(ID + " Signed up for AtDestination event");
          movement.AtDestination += HandleAtDestination;
          signedUpOnMovement = true;
          SafetyCheck();
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

  /// <summary>
  /// Enter the action again but keeps the same target
  /// </summary>
  protected void ReEnter()
  {
    distanceFromTarget = Vector2.Distance(owner.transform.position, target);
    Debug.LogWarning("ReEntered action: " + ID + " InRange = " + IsInRange() + " distance = " + distanceFromTarget);

    base.Enter();


  }

  void HandleAtDestination(object sender, EventArgs e)
  {
    SafetyCheck();
    movement.PrintAtTargetInvocationList();
    movement.AtDestination -= HandleAtDestination;
    signedUpOnMovement = false;


    Debug.LogWarning(ID + " Removed from AtDestination event");

    ReEnter();
  }

  public abstract bool IsInRange();

  public override void Execute()
  {
    base.Execute();
  }

  protected override void Successfull()
  {
    if (signedUpOnMovement)
    {
      movement.AtDestination -= HandleAtDestination;
      signedUpOnMovement = false;
    }
    base.Successfull();
  }


  protected override void Failed()
  {

    if (signedUpOnMovement)
    {
      movement.AtDestination -= HandleAtDestination;
      signedUpOnMovement = false;
    }
    base.Failed();
  }

  /// <summary>
  /// Händer detta så har något falerat miserabelt och måste tittas närmare på!
  /// </summary>
  private void SafetyCheck()
  {
    try
    {
      if (movement.GetTargetInvocationCount() > 1)
      {
        throw (new Exception("To many Actions have signed up on " +
          "Movement callback event. Must be fixed!"));
      }
    }
    catch (Exception ex)
    {
      Debug.LogError(ex.Message);
      throw;
    }
  }
}
