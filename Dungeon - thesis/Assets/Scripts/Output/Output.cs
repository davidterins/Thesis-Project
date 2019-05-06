using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Output
{
  public static List<RoomCardModel> Cards = new List<RoomCardModel>();
  public static Dictionary<int, List<RoomCardModel>> CardsPerRoom = new Dictionary<int, List<RoomCardModel>>();
  public static int CurrentIteration = 1;
  private static int totalIterations = 1;
  static float totOpinion;

  public static void RegisterCard(RoomCardModel card)
  {
    var iteration = GameController.GameCurrentIteration;
    if (CardsPerRoom.ContainsKey(card.RoomID))
    {
      CardsPerRoom[card.RoomID].Add(card);
    }
    else
    {
      CardsPerRoom[card.RoomID] = new List<RoomCardModel>
      {
        card
      };
    }

    Cards.Add(card);
  }

  public static List<RoomCardModel> GetSumarizedRoomCards()
  {
    List<RoomCardModel> summaryList = new List<RoomCardModel>();

    foreach (var key in CardsPerRoom.Keys)
    {
      var summarizedRoomCardModel = new RoomCardModel
      {
        RoomID = key
      };

      float summarizedRoomRating = 0;
      float lowestRating = float.MaxValue;
      float highestRating = float.MinValue;
      int completeCount = 0;

      foreach (RoomCardModel card in CardsPerRoom[key])
      {
        if((string)card.OutputValuePairs["RoomStatus"] == "Completed")
        {
          var roomRating = (float)card.OutputValuePairs["Opinion"];
          if (roomRating < lowestRating)
          {
            lowestRating = roomRating;
          }
          if (roomRating > highestRating)
          {
            highestRating = roomRating;
          }
          summarizedRoomRating += roomRating;
          completeCount++;
        }
      }

      var avgRating = summarizedRoomRating / completeCount;
      summarizedRoomCardModel.WriteTo("Avg rating", avgRating);
      //Median om det har körts fler än 5 iterationer.
      if (completeCount > 5)
      {
        var medianRating = 0.0;

        if (completeCount % 2 == 0)
        {
          medianRating = 
            ((float)CardsPerRoom[key][completeCount / 2].OutputValuePairs["Opinion"] +
            (float)CardsPerRoom[key][(completeCount / 2) + 1].OutputValuePairs["Opinion"]) / 2;
        }
        else
        {
          medianRating = (float)CardsPerRoom[key][completeCount / 2].OutputValuePairs["Opinion"];
        }

        summarizedRoomCardModel.WriteTo("Median rating", medianRating);
      }

      summarizedRoomCardModel.WriteTo("Low", lowestRating);
      summarizedRoomCardModel.WriteTo("High", highestRating);

      summarizedRoomCardModel.WriteTo("Completion rate: ", completeCount + "/" + CardsPerRoom[key].Count);

      summaryList.Add(summarizedRoomCardModel);
    }

    return summaryList;
  }

  private static float DungeonSummaryRating()
  {
    float totalOpinion = 0;

    foreach (RoomCardModel card in Cards)
    {
      totalOpinion += (float)card.OutputValuePairs["Opinion"];
    }

    totalOpinion = totalOpinion / Cards.Count;
    return totalOpinion;
  }

  public static SummaryCardModel DungeonSummaryCard()
  {

    var cardModel = new SummaryCardModel();
    var dungeonRating = DungeonSummaryRating();
    //TODO ta fram alla värden som ett summary card ska ha och skriv ner dom här.
    cardModel.WriteTo("Opinion", dungeonRating);
    //float finalOpinionValue = totOpinion / totalIterations;
    //cardModel.WriteTo("Summarized opinion for: " + totalIterations + " runs", finalOpinionValue);

    return cardModel;
  }
}




