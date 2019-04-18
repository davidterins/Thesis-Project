using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;

public enum Personality { BRAVERY, BLOODLUST, GREED, EXPLORATION, PROGRESSION, }

public abstract class Persona : MonoBehaviour
{
  protected float finalOpinion = 0f;
  protected int HpEnteringRoom;
  protected RoomCardModel currentCard;
  protected float minInteractionsToGetKeys;

  public Dictionary<Personality, float> personalityModifer = new Dictionary<Personality, float>
    {
      //{ Personality.BRAVERY, 0f },
      { Personality.BLOODLUST, 0f },
      { Personality.GREED, 0f },
      //{ Personality.EXPLORATION, 0f },
      { Personality.PROGRESSION, 0f }
    };

  protected Dictionary<string, object> OutPutPairs = new Dictionary<string, object>();
  public float enemyDistanceRange; // The distance of even being interested in an enemy

  protected virtual void Start()
  {
    enemyDistanceRange = GetComponent<Vision>().GetSightRange() - 1f;

    PrepareForNewRoom(new RoomCardModel());

    Enemy.OnEnemyDeath += HandleOnEnemyDeath;
    TreasureChest.OnTreasureLoot += HandleOnTreasureLoot;
    Door.OnRoomEnter += HandleOnRoomEnter;
  }

  /// <summary>
  /// Calculate the final opinion of the current room before entering the next one
  /// and register it in the output. After that the values are reset.
  /// </summary>
  void HandleOnRoomEnter()
  {
    WriteOutPutCard("Completed");
    //Create a card for the new room
    PrepareForNewRoom(new RoomCardModel());
  }

  public void WriteOutPutCard(string roomStatus)
  {
    //Save card and register it to output
    //OutPutPairs["Opinion"] = CalculateFinalOpinion();
    //OutPutPairs["RoomStatus"] = roomStatus;

    currentCard.WriteTo("Opinion", CalculateFinalOpinion());
    currentCard.WriteTo("RoomStatus", roomStatus);
    foreach (var pair in OutPutPairs)
    {
      currentCard.WriteTo(pair.Key, pair.Value);
    }

    Output.RegisterCard(currentCard);
  }

  protected abstract void HandleOnTreasureLoot();

  protected abstract void HandleOnEnemyDeath();

  protected abstract float CalculateFinalOpinion();

  // Prepare the agent values for a new room.
  // Happens either in awake or when the agent has entered a door to a new room.
  protected virtual void PrepareForNewRoom(RoomCardModel newCard)
  {
    currentCard = newCard;
    currentCard.RoomID = Dungeon.Singleton.CurrentRoom.RoomID;

    minInteractionsToGetKeys = Dungeon.Singleton.CurrentRoom.requiredKeys.Count;
    HpEnteringRoom = GetComponent<Player>().Health;
  }

  private void OnDestroy()
  {
    Enemy.OnEnemyDeath -= HandleOnEnemyDeath;
    TreasureChest.OnTreasureLoot -= HandleOnTreasureLoot;
    Door.OnRoomEnter -= HandleOnRoomEnter;
  }

}
