using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILootableObject
{
  List<GameObject> Loot { get; set; }
}
