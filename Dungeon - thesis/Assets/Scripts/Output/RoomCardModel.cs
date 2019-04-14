using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCardModel : CardModel
{
  public int RoomID { get; set; }
}

public class SummaryCardModel : CardModel
{
  public string Header { get { return "Summary"; } }
}

public abstract class CardModel
{
  Dictionary<string, object> outputValuePairs = new Dictionary<string, object>();
  public Dictionary<string, object> OutputValuePairs { get { return outputValuePairs; } }

  // Object should be something the can be printed as text.
  public void WriteTo(string cardHeader, object note)
  {
    outputValuePairs.Add(cardHeader, note);
  }
}
