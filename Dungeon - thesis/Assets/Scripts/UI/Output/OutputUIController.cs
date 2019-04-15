using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutputUIController : MonoBehaviour
{
  [SerializeField]
  GameObject UICardPrefab = null;

  [SerializeField]
  GameObject OutputMenu = null;

  [SerializeField]
  GameObject CardContainer = null;

  [SerializeField]
  ActionButton MainMenuButton = null;

  [SerializeField]
  ActionButton RestartButton = null;

  
  private void Awake ()
  {
    RestartButton.AssignAction(GameController.Singleton.Restart);
    MainMenuButton.AssignAction(GoToMainMenu);
    GameController.OnShowOutput += Show;
  }

  void GoToMainMenu()
  {
    Output.Cards = new List<RoomCardModel>();
    SceneManager.LoadScene("StartupScene");
  }

  public void Show()
  {
    foreach(Transform child in CardContainer.transform)
    {
      Destroy(child.gameObject);
    }


    OutputMenu.SetActive(true);
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

  private void OnDestroy()
  {
    GameController.OnShowOutput -= Show;
  }

}
