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
  public Dictionary<Personality, float> personalityModifer = new Dictionary<Personality, float>
    {
      { Personality.BRAVERY, 0f },
      { Personality.BLOODLUST, 0f },
      { Personality.GREED, 0f },
      { Personality.EXPLORATION, 0f },
      { Personality.PROGRESSION, 0f }
    };
  public float enemyDistanceRange; // The distance of even being interested in an enemy

  protected virtual void Awake()
  {
    currentCard = new RoomCardModel();
    currentCard.RoomID = Dungeon.Singleton.CurrentRoom.RoomID;

    HpEnteringRoom = GetComponent<Player>().Health;
    enemyDistanceRange = GetComponent<Vision>().GetSightRange() - 1f;

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
    ResetValues();
    currentCard = new RoomCardModel();
    currentCard.RoomID = Dungeon.Singleton.CurrentRoom.RoomID;
  }

  public void WriteOutPutCard(string roomStatus)
  {
    //Save card and register it to output
    float opinion = CalculateFinalOpinion();

    currentCard.WriteTo("Opinion", opinion);
    currentCard.WriteTo("RoomStatus", roomStatus);
    Debug.LogError(GetType() + " OPINION: " + opinion);
    Output.RegisterCard(currentCard);
  }

  protected abstract void HandleOnTreasureLoot();

  protected abstract void HandleOnEnemyDeath();

  protected abstract float CalculateFinalOpinion();

  protected abstract void ResetValues();

  private void OnDestroy()
  {
    Enemy.OnEnemyDeath -= HandleOnEnemyDeath;
    TreasureChest.OnTreasureLoot -= HandleOnTreasureLoot;
    Door.OnRoomEnter -= HandleOnRoomEnter;
  }

}
