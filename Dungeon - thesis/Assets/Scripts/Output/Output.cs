using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Output
{
  public static List<Card> Cards = new List<Card>();

  public static void RegisterCard(Card card)
  {
    Cards.Add(card);
  }

  public static void EvaluateCards()
  {
    float totalOpinion = 0;
    foreach(Card card in Cards)
    {
      totalOpinion += card.Opinion;
    }
    totalOpinion = totalOpinion / Cards.Count;

    Debug.Log("Final dungeon opinion: " + totalOpinion);
  }
}

public struct Card
{
  public float Opinion { get; set; }
}

