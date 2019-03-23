using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem_Action : MovingAction_Goap
{
  List<GameObject> lootItems;

  public PickupItem_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.PickupItem;

    PreConditions = new WorldStateSymbol[]
    {
      WorldStateSymbol.LootableItem
    };

    Effects = new WorldStateSymbol[]
    {
     WorldStateSymbol.HasItem,
     WorldStateSymbol.HasPotion,
     WorldStateSymbol.HasKey,
     WorldStateSymbol.HasAmmo,
     WorldStateSymbol.HasMeleeWeapon,
     WorldStateSymbol.HasRangedWeapon,
     };
  }

  public override void Enter()
  {
    lootItems = owner.GetComponent<BlackBoard>().TargetLoot;
   
    if (lootItems == null)
    {
      Failed();
    }
    else
    {
      target = lootItems[0].transform.position;
      base.Enter();
    }
  }

  public override void Execute()
  {
    if (InRange)
    {
      base.Execute();
      foreach(GameObject loot in lootItems)
      {
        loot.GetComponent<InteractableObject>().Interact(owner);
      }
      // Is set here for now, should probably the loot handle this when picked up.
      owner.GetComponent<BlackBoard>().TargetLoot = null;
      //owner.GetComponent<BlackBoard>().ImportantItemDrop = null;
      Successfull();
    }

  }

  public override bool IsInRange()
  {
    InRange = Vector2.Distance(owner.transform.position, target) <= interactionRange + 0.5;
    return InRange;
  }
}
