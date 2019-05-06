using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IterationInfo : MonoBehaviour
{
  [SerializeField]
  Text IterationText = null;
  // Start is called before the first frame update
  void Start()
  {

    GameController.OnIterationChanged += Output_OnIterate;
    IterationText.text = "Remaining iterations: " + GameController.GameCurrentIteration;
  }

  void Output_OnIterate(int iteration)
  {
    IterationText.text = "Remaining iterations: " + iteration;
  }


  private void OnDestroy()
  {
    GameController.OnIterationChanged -= Output_OnIterate;
  }
}
