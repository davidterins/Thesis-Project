using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GoapViewController : MonoBehaviour
{
  [SerializeField]
  private Text GoalText = null;

  [SerializeField]
  public GameObject PlanList;

  [SerializeField]
  public Text PlanStepTextPrefab;

  [SerializeField]
  private Text WSVariableText = null;


  int actionIndex = 0;

  public void SetGoal(Goal_Goap goal)
  {
    GoalText.text = "Goal:\n" + goal.GetType() + " " + goal.Relevancy;

  }

  public void SetPlan(Queue<ActionID> actions)
  {
    foreach (Transform child in PlanList.transform)
    {
      Destroy(child.gameObject);
    }

    actionIndex = 0;
    int index = 1;
    foreach (ActionID action in actions)
    {
      var planStep = Instantiate(PlanStepTextPrefab, PlanList.transform);
      planStep.color = Color.yellow;
      planStep.text +=  index++ + ". " + action;
    }

  }

  public void UpdateActionStatus(ActionCallback actionResult)
  {
    //Debug.Log("Current acttionIndex " + actionIndex);
    var updatedplanStep = PlanList.transform.GetChild(actionIndex).gameObject;
    switch (actionResult)
    {
      case ActionCallback.Successfull:
        updatedplanStep.GetComponent<Text>().color = Color.green;
        break;
      case ActionCallback.Failed:
        updatedplanStep.GetComponent<Text>().color = Color.red;
        break;
    }
    actionIndex++; 
  }

  public void UpdateWSVariables(WorldStateSet currentWS)
  {
    WSVariableText.text = "WorldState";
    foreach (var keyVal in currentWS)
    {
      WSVariableText.text += "\n" + keyVal.Key + ": " + keyVal.Value;
    }
  }
}

