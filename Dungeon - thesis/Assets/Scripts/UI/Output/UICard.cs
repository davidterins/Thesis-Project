using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICard : MonoBehaviour
{
  [SerializeField]
  Text HeaderText;

  [SerializeField]
  Text BodyText;

  public void SetCardValues(RoomCardModel card)
  {
    HeaderText.text += " " + card.RoomID;

    BodyText.text += "\n";
    foreach (var keyval in card.OutputValuePairs)
    {
      BodyText.text += keyval.Key + ": " + keyval.Value + "\n";
    }
  }

  public void SetCardValues(SummaryCardModel card)
  {
    HeaderText.text = card.Header;

    BodyText.text += "\n";
    foreach (var keyval in card.OutputValuePairs)
    {
      BodyText.text += keyval.Key + ": " + keyval.Value + "\n";
    }
  }
}
