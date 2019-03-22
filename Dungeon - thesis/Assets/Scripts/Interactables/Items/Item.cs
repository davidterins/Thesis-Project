using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : InteractableObject
{
  [SerializeField]
  Sprite itemSprite;

  public Sprite ItemSprite { get { return itemSprite; } }

  // TODO put this in use somehow
  protected int importance = 0;
  public int Importance { get { return importance; } }

  protected float dropRate = 0.2f;
  public float Droprate { get { return dropRate; } }

  public override void Interact(GameObject player)
  {
    player.GetComponent<Agent>().HandlePickup(this);

    Destroy(gameObject, 1);
  }

  public virtual float GetDropRate()
  {
    return dropRate;
  }

  public virtual float GetImportance()
  {
    return importance;
  }


  public virtual void Use()
  {

  }
}

