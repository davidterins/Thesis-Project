using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Output
{
  public static List<RoomCardModel> Cards = new List<RoomCardModel>();

  public static void RegisterCard(RoomCardModel card)
  {
    Cards.Add(card);
  }

  private static float TotalOpinion()
  {
    float totalOpinion = 0;

    foreach(RoomCardModel card in Cards)
    {
      totalOpinion += (float)card.OutputValuePairs["Opinion"];
    }
    totalOpinion = totalOpinion / Cards.Count;

    return totalOpinion;
  }

  public static SummaryCardModel DungeonSummaryCard()
  {
    var cardModel = new SummaryCardModel();
    //TODO ta fram alla värden som ett summary card ska ha och skriv ner dom här.
    cardModel.WriteTo("Opinion", TotalOpinion());
   
    return cardModel;
  }
}




