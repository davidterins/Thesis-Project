using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest_Action : MovingAction_Goap
{
  GameObject targetItem;

  public OpenChest_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.OpenChest;

    PreConditions = new WorldStateSymbol[]
   {
      WorldStateSymbol.AvailableChest
   };

    Effects = new WorldStateSymbol[]
    {
     WorldStateSymbol.LootableItem
     };
  }

  public override void Enter()
  {
    targetItem = owner.GetComponent<BlackBoard>().TreasureObject;
    if (!targetItem)
    {
      Failed();
    }
    else
    {
      target = targetItem.transform.position;
      //Debug.Log("Targeted Chest pos: " + target.x + ", " + target.y);
      base.Enter();
    }
  }

  public override void Execute()
  {
    if (InRange)
    {
      base.Execute();
      targetItem.GetComponent<InteractableObject>().Interact(owner);
      Successfull();
    }
  }

  public override bool IsInRange()
  {
    InRange = Vector2.Distance(owner.transform.position, targetItem.transform.position) <= 0.5f;
    return InRange;
  }

  public override float GetCost()
  {
    return 1;
  }

}
