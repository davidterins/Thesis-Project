using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILootableObject
{
  Loot Loot { get; }
  void AddLoot(GameObject gameObject);
}
