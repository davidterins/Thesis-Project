using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Output
{
  public static List<RoomCardModel> Cards = new List<RoomCardModel>();

  public static int currentIteration = 50;
  private static int totIterations = 50;
  static float totOpinion;

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
    float finalOpinionValue = totOpinion / totIterations;
    cardModel.WriteTo("Summarized opinion for: " + totIterations + " runs", finalOpinionValue);

    return cardModel;
  }

  public static void DecreaseTotalRuns()
  {
    currentIteration--;
    totOpinion += TotalOpinion();

    Debug.Log("At iteration " + currentIteration);

  }

}




