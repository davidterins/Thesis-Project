using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

/// <summary>
/// Rusher persona.
/// 0 är dåligt, 1 är bra.
/// </summary>
public class RusherPersona : Persona
{
  private float forcedCombat = 1, hpLeft = 1, forcedLoot = 1;

  private float forcedCombatWeight;
  private float forcedTreasureWeight;

  private int startHP;

  Card currentCard;

  public override void Awake()
  {
    base.Awake();
    enemyDistanceRange = GetComponent<Vision>().GetSightRange() - 1f;
    personalityModifer[Personality.BRAVERY] = 0.4f;
    personalityModifer[Personality.BLOODLUST] = 0.4f;
    personalityModifer[Personality.GREED] = 0.1f;
    personalityModifer[Personality.EXPLORATION] = 0.3f;
    personalityModifer[Personality.PROGRESSION] = 0.9f;

    Enemy.OnEnemyDeath += Enemy_OnEnemyDeath;
    TreasureChest.OnTreasureLoot += TreasureChest_OnTreasureLoot;
    Door.OnRoomEnter += Door_OnRoomEnter;

    currentCard = new Card();
    startHP = GetComponent<Player>().Health;
    forcedCombatWeight = 4f / Dungeon.Singleton.CurrentRoom.RoomGraph.TotalTraversableTiles;
    forcedTreasureWeight = 3f / Dungeon.Singleton.CurrentRoom.RoomGraph.TotalTraversableTiles;

  }

  void Door_OnRoomEnter()
  {
    //SaveCard
    currentCard.Opinion = CalculateFinalOpinion();

    Debug.LogError("OPINION: " + currentCard.Opinion);
    Output.RegisterCard(currentCard);
    //Create new card
    ResetValues();
    currentCard = new Card();

  }

  void TreasureChest_OnTreasureLoot()
  {
    forcedLoot -= forcedTreasureWeight;
  }

  void Enemy_OnEnemyDeath()
  {
     forcedCombat -= forcedCombatWeight;
  }


  private float CalculateFinalOpinion()
  {
    forcedCombat = Mathf.Clamp(forcedCombat, 0, 1);
    forcedLoot = Mathf.Clamp(forcedLoot, 0, 1);
    hpLeft = Mathf.Clamp(1 - (GetComponent<Player>().Health / startHP), 0, 1);
    

    return (forcedCombat + hpLeft + forcedLoot) / 3;
  }

  private void ResetValues()
  {
    forcedCombat = 1;
    hpLeft = 1; 
    forcedLoot = 1;
    startHP = GetComponent<Player>().Health;
  }
}
