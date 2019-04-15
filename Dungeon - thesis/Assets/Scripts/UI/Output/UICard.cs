using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UICard : MonoBehaviour
{
  [SerializeField]
  Text HeaderText = null;

  [SerializeField]
  Text BodyText = null;

  public void SetCardValues(RoomCardModel card)
  {
    HeaderText.text += " " + card.RoomID;

    BodyText.text = "";
    foreach (var keyval in card.OutputValuePairs)
    {
      string value = keyval.Value.ToString();
      if (keyval.Value is float)
      {
        if(value.Length > 4)
        {
          value = value.Substring(0, 4);
        }
      }
      BodyText.text += keyval.Key + ": " + value + "\n";
    }
  }

  public void SetCardValues(SummaryCardModel card)
  {
    HeaderText.text = card.Header;

    BodyText.text = "";
    foreach (var keyval in card.OutputValuePairs)
    {
      string value = keyval.Value.ToString();
      if (keyval.Value is float)
      {
        if (value.Length > 4)
        {
          value = value.Substring(0, 4);
        }
      }
      BodyText.text += keyval.Key + ": " + value + "\n";
    }
  }

}
