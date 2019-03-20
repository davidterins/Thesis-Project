using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.UIElements;

public class Key : Item
{
  private void Awake()
  {
    importance = 1;
    //dropRate = 1;
    //KeyData = GameObject.FindWithTag("Dungeon").GetComponent<Dungeon>().CurrentRoom.requiredKeys.Dequeue();
  }

  public override void Interact(GameObject player)
  {
    player.GetComponent<Agent>().HandlePickup(this);
    player.GetComponent<BlackBoard>().ImportantItemDrop = null;
    Destroy(gameObject, 1);
  }

  public KeyInfo KeyData {get; set;}

  public override void Use()
  {
    base.Use();
  }

  public override float GetDropRate()
  {
    return 1;
  }

  public override float GetImportance()
  {
    return 1;
  }
}

public struct KeyInfo
{
  public Door TargetDoor { get; private set; }
  public KeyInfo(Door door)
  {
    TargetDoor = door;
  }
}
