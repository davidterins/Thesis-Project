using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
  private void Start()
  {
    importance = 1;
   
  }

  public override void Interact(GameObject player)
  {
    player.GetComponent<Agent>().HandlePickup(this);
    player.GetComponent<BlackBoard>().ImportantItemDrop = null;
    Destroy(gameObject, 1);
  }

  public Door TargetDoor { get; set; }

  public override void Use()
  {
    base.Use();

  }



}
