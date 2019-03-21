using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Agent
{
  public override void TakeDamage(GameObject attacker, int amount)
  {
    base.TakeDamage(attacker, amount);
    attacker.GetComponent<Agent>().TakeDamage(gameObject, Damage);
  }

  protected override void HandleDeath(GameObject attacker)
  {
    var dungeon = GameObject.FindWithTag("Dungeon").GetComponent<Dungeon>();
    var loot = transform.GetChild(0);
    loot.transform.SetParent(dungeon.GetRoomObject(dungeon.CurrentRoom.RoomID).transform);
    loot.GetComponent<Loot>().Drop(attacker, transform.position);



    base.HandleDeath(attacker);
  }
}
