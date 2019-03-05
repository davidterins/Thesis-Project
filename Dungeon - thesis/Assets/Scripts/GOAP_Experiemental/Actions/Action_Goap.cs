using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Goap
{
  public event EventHandler<ActionFinishedEventArgs> ActionCallback;

  protected Action action;

  public int cost = 1;

  public ActionID ID;

  public WorldState[] Effects { get; protected set; }

  public WorldState[] PreConditions { get; protected set; }

  public Action_Goap(Action action)
  {
    Effects = new WorldState[0];
    PreConditions = new WorldState[0];
    this.action = action;
  }

  public virtual void ExecuteAction()
  {
    action.Invoke();
  }

  public bool ActionCompleted { get; private set; }


  public WorldStateSet ApplyEffects(WorldStateSet worldState)
  {
    WorldStateSet appliedWorldState = (WorldStateSet)worldState.Clone();

    foreach(WorldState effect in Effects)
    {
      appliedWorldState[effect] = true;
    }
    return appliedWorldState;
  }

  public bool IsValidInWorldState(WorldStateSet worldState)
  {
    foreach(WorldState precondition in PreConditions)
    {
      if (!worldState[precondition])
        return false;

    }
    return true;
  }
}



