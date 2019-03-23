using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Item : InteractableObject
{
  [SerializeField]
  Sprite itemSprite;

  public event Action<Item> OnItemUse = delegate { };

  public Sprite ItemSprite { get { return itemSprite; } }
  public WorldStateSymbol ItemWSEffector { get { return GetItemWSEffector(); } }

  // TODO put this in use somehow
  protected int importance = 0;
  public int Importance { get { return importance; } }

  protected float dropRate = 0.2f;
  public float Droprate { get { return dropRate; } }

  public override void Interact(GameObject player)
  {
    player.GetComponent<Player>().PickupItem(this);

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

  protected virtual WorldStateSymbol GetItemWSEffector()
  {
    return WorldStateSymbol.HasItem;
  }


  protected void Used()
  {
    OnItemUse.Invoke(this);
  }
}

