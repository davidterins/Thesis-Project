using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoapViewController : MonoBehaviour
{
  [SerializeField]
  private Text GoalText;

  [SerializeField]
  public Text PlanText;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetGoal(Goal_Goap goal)
  {
    GoalText.text = "Goal:\n" + goal.GetType();

  }

  public void SetPlan(Stack<ActionID> actions)
  {
    PlanText.text = "Plan:";

    int index = 1;
    while (actions.Count > 0)
      PlanText.text += "\n" + index++ + ". " + actions.Pop();

  }
}
