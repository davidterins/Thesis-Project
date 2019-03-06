using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Action finished event arguments.
/// </summary>
public class ActionFinishedEventArgs : EventArgs
{
  public ActionCallback Result { get; private set; }

  public ActionFinishedEventArgs(ActionCallback actionResult)
  {
    Result = actionResult;
  }
}
