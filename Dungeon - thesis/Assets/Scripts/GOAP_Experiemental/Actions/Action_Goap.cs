using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Goap
{
  public virtual event EventHandler<ActionFinishedEventArgs> OnActionFinished;

  protected readonly GameObject owner;
  protected FSM _FSM;

  /// <summary>
  /// Dessa tre måste sättas i konstruktorn i varje action.
  /// </summary>
  public int cost = 1;
  public ActionID ID;
  public WorldState[] Effects { get; protected set; }
  public WorldState[] PreConditions { get; protected set; }

  public Action_Goap(GameObject owner, FSM FSM)
  {
    Effects = new WorldState[0];
    PreConditions = new WorldState[0];
    this.owner = owner;
    _FSM = FSM;
  }

  public virtual void Enter()
  {
    Debug.Log("Entered: " + ID);
  }

  public virtual void Execute()
  {

  }

  protected void Interrupted()
  {
    OnActionFinished.Invoke(this, new ActionFinishedEventArgs(ActionCallback.Failed));
  }

  protected void Successfull()
  {
    OnActionFinished.Invoke(this, new ActionFinishedEventArgs(ActionCallback.Successfull));
  }

  protected void RunAgain()
  {
    OnActionFinished.Invoke(this, new ActionFinishedEventArgs(ActionCallback.RunAgain));
  }

  public WorldStateSet ApplyEffects(WorldStateSet worldState)
  {
    WorldStateSet appliedWorldState = (WorldStateSet)worldState.Clone();

    foreach (WorldState effect in Effects)
    {
      appliedWorldState[effect] = true;
    }
    return appliedWorldState;
  }

  public bool IsValidInWorldState(WorldStateSet worldState)
  {
    foreach (WorldState precondition in PreConditions)
    {
      if (!worldState[precondition])
        return false;
    }
    return true;
  }
}



