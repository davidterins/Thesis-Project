using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputUIController : MonoBehaviour
{
  [SerializeField]
  GameObject UICardPrefab = null;

  [SerializeField]
  GameObject CardContainer = null;

  private void Start()
  {
    GameController.OnShowOutput += Show;
  }


  public void Show()
  {
    CardContainer.SetActive(true);
    // Create cards for each room
    foreach(RoomCardModel card in Output.Cards)
    {
      var UICard = Instantiate(UICardPrefab, CardContainer.transform);
      UICard.GetComponent<UICard>().SetCardValues(card);
    }

    // Create card for dungeon summary
    var UISummaryCard = Instantiate(UICardPrefab, CardContainer.transform);
    UISummaryCard.GetComponent<UICard>().SetCardValues(Output.DungeonSummaryCard());
   
  }

}
