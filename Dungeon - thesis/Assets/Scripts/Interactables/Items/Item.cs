using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : InteractableObject
{
  public override void Interact(GameObject player)
  {
    player.GetComponent<Agent>().HandlePickup(this);
    Destroy(gameObject, 1);
  }
}

