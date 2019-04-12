using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputUIController : MonoBehaviour
{
  [SerializeField]
  GameObject CardPrefab = null;

  [SerializeField]
  GameObject CardContainer = null;

  private void Start()
  {
    GameController.OnShowOutput += Show;
  }


  public void Show()
  {
    CardContainer.SetActive(true);

    foreach(Card card in Output.Cards)
    {
      Instantiate(CardPrefab, CardContainer.transform);
    }
  }

}
