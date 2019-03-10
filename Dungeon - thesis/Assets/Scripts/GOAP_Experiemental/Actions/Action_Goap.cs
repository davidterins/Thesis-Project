using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Action_Goap
{
  public virtual event EventHandler<ActionFinishedEventArgs> OnActionFinished;

  protected readonly GameObject owner;

  /// <summary>
  /// Dessa tre måste sättas i konstruktorn i varje action.
  /// </summary>
  public int cost = 1;
  public ActionID ID;
  public WorldStateSymbol[] Effects { get; protected set; }
  public WorldStateSymbol[] PreConditions { get; protected set; }

  public Action_Goap(GameObject owner)
  {
    Effects = new WorldStateSymbol[0];
    PreConditions = new WorldStateSymbol[0];
    this.owner = owner;
  }

  public virtual void Enter()
  {
    Debug.Log("Entered: " + ID);
  }

  public virtual void Execute() { }

  protected virtual void Exit() { }

  protected void Failed()
  {
    Debug.Log(ID + " was a failure");
    OnActionFinished.Invoke(this, new ActionFinishedEventArgs(ActionCallback.Failed));
  }

  protected void Successfull()
  {
    Debug.Log(ID + " was sucessful");
    OnActionFinished.Invoke(this, new ActionFinishedEventArgs(ActionCallback.Successfull));
  }

  protected void NeedPath()
  {
    Debug.Log(ID + " was sucessful");
    OnActionFinished.Invoke(this, new ActionFinishedEventArgs(ActionCallback.NeedPath));
  }

  public WorldStateSet ApplyEffects(WorldStateSet worldState)
  {
    WorldStateSet appliedWorldState = (WorldStateSet)worldState.Clone();

    foreach (WorldStateSymbol effect in Effects)
    {
      appliedWorldState[effect] = true;
    }
    return appliedWorldState;
  }

  public bool IsValidInWorldState(WorldStateSet worldState)
  {
    foreach (WorldStateSymbol precondition in PreConditions)
    {
      if (!worldState[precondition])
        return false;
    }
    return true;
  }
}



