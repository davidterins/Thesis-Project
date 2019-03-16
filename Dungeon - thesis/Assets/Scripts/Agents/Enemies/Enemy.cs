using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Agent
{
  [SerializeField]
  List<GameObject> Loot = null;


  protected override void HandleDeath(GameObject attacker)
  {
    var tempLootList = new List<GameObject>(Loot.Count);
    foreach (var item in Loot)
    {
      tempLootList.Add(Instantiate(item, transform.position, Quaternion.identity));
    }

    attacker.GetComponent<BlackBoard>().TargetLoot = tempLootList;

    base.HandleDeath(attacker);
  }
}
