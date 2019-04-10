using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Enemy : Agent, IMemorizable
{
  public static event Action OnEnemyDeath = delegate { };

  private void Awake()
  {
    RoomID = Dungeon.Singleton.CurrentRoom.RoomID;
  }

  public int RoomID { get; private set; }

  public Type MemorizableType { get { return GetType(); } }

  bool ofInterest = true;
  public bool OfInterest { get { return ofInterest; } set { ofInterest = value; } }

  public override void TakeDamage(GameObject attacker, int amount)
  {
    base.TakeDamage(attacker, amount);
    attacker.GetComponent<Agent>().TakeDamage(gameObject, Damage);
  }

  protected override void HandleDeath(GameObject attacker)
  {
    var dungeon = Dungeon.Singleton;
    var loot = transform.GetChild(0);
    loot.transform.SetParent(dungeon.GetRoomObject(dungeon.CurrentRoom.RoomID).transform);
    loot.GetComponent<Loot>().Drop(attacker, transform.position);

    attacker.GetComponent<BlackBoard>().RemoveTypePOI(GetType(), gameObject);

    OnEnemyDeath.Invoke();
    base.HandleDeath(attacker);
  }
}
