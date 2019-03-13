using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Agent
{
  [SerializeField]
  GameObject[] Loot = null;


  protected override void HandleDeath(GameObject attacker)
  {
    attacker.GetComponent<BlackBoard>().TargetLoot.Clear();
    foreach (var item in Loot)
    {
      attacker.GetComponent<BlackBoard>().TargetLoot.Add(
      Instantiate(item, transform.position, Quaternion.identity, transform.parent));
    }

    base.HandleDeath(attacker);
  }
}
