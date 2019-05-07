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

  protected MovingAction_Goap(GameObject owner) : base(owner) { }

  public override void Enter()
  {
    //if(movement)
    //{
    //  movement.AtDestination -= HandleAtDestination;
    //}
    base.Enter();
  }

  protected override bool CanExecute()
  {
    if (base.CanExecute())
    {
      if (!IsInRange() && this == owner.GetComponent<Goap_Controller>().currentAction)
      {
        movement = owner.GetComponent<Movement>();
        Debug.Log(GetType() + " Target was not in range, walk to target " + Vector2.Distance(owner.transform.position, target));
        if (movement.TryMoveToTarget(target, interactionRange))
        {
          movement.AtDestination += HandleAtDestination;
          Debug.LogWarning(ID + " Signed up for AtDestination event");
          SafetyCheck();
        }
        else
        {
          Debug.Log(GetType() + " No Path to target: [" + target.x + "," + target.y +"]");
          return false;
        }
      }
    }
    else
    {
      return false;
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

  public void Unload()
  {
    movement.AtDestination -= HandleAtDestination;
  }

  void HandleAtDestination(object sender, EventArgs e)
  {
    SafetyCheck();
    movement.PrintAtTargetInvocationList();
    movement.AtDestination -= HandleAtDestination;


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
    //if (signedUpOnMovement)
    //{
    //  movement.AtDestination -= HandleAtDestination;
    //  OnActionSignedOfFromMovement.Invoke(ID);
    //  signedUpOnMovement = false;
    //}
    base.Successfull();
  }


  protected override void Failed()
  {

    //if (signedUpOnMovement)
    //{
    //  movement.AtDestination -= HandleAtDestination;
    //  OnActionSignedOfFromMovement.Invoke(ID);
    //  signedUpOnMovement = false;
    //}
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
