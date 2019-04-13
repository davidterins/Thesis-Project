using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionDebug : MonoBehaviour
{

  [SerializeField]
  Text activeActionsText;

  private List<ActionID> activeActions = new List<ActionID>(5);

  private void Start()
  {
    //MovingAction_Goap.OnActionSignedUpOnMovement += Action_Goap_OnActiveChangedDebug;
    //MovingAction_Goap.OnActionSignedOfFromMovement += Action_Goap_OnActionDeActivated;
  }


  void Action_Goap_OnActionDeActivated(ActionID ID)
  {
    activeActionsText.text = "";
    activeActions.Remove(ID);
    foreach (ActionID iD in activeActions)
    {
      activeActionsText.text += iD + "\n";
    }
  }


  void Action_Goap_OnActiveChangedDebug(ActionID ID)
  {
    activeActionsText.text = "";
    activeActions.Add(ID);
    foreach (ActionID iD in activeActions)
    {
      activeActionsText.text += iD + "\n";
    }
  }





}
