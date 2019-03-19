using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Agent, ILootableObject
{
  [SerializeField]
  List<GameObject> Loot = null;

  public void AddLoot(GameObject gameObject)
  {
    Loot.Add(gameObject);
  }

  protected override void HandleDeath(GameObject attacker)
  {
    var tempLootList = new List<GameObject>(Loot.Count);
    foreach (var item in Loot)
    {
      float dropRateValue = Random.Range(0.00f, 1.00f);

      if (dropRateValue <= item.GetComponent<Item>().Droprate)
      {
        var lootObj = Instantiate(item, transform.position, Quaternion.identity);
        tempLootList.Add(lootObj);

       
      }
      if (item.GetComponent<Key>())
      {
        var lootObj = Instantiate(item, transform.position, Quaternion.identity);
        tempLootList.Add(lootObj);
        attacker.GetComponent<BlackBoard>().ImportantItemDrop = lootObj;
      }
    }

    if (tempLootList.Count > 0)
      attacker.GetComponent<BlackBoard>().TargetLoot = tempLootList;

    base.HandleDeath(attacker);
  }
}
