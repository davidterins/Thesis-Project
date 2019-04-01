using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ActionButton : Button
{
  Action buttonAction;

  public event Action buttonAc = delegate { };

  public void AssignAction(Action action)
  {
    buttonAc = action;
  }

  public override void OnPointerClick(PointerEventData eventData)
  {
    base.OnPointerClick(eventData);
    buttonAc.Invoke();
  }

}
