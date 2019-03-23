using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor_Action : MovingAction_Goap
{

  GameObject targetDoor;
  Key key;

  public OpenDoor_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.OpenDoor;

    PreConditions = new WorldStateSymbol[]
   {
      WorldStateSymbol.HasKey
   };

    Effects = new WorldStateSymbol[]
    {
     WorldStateSymbol.Progress
     };
  }

  public override void Enter()
  {
    Item item;
    if (owner.GetComponent<Inventory>().TryGetItem(typeof(Key), out item))
    {
      key = (Key)item;
      targetDoor = key.KeyData.TargetDoor.gameObject;
      target = targetDoor.transform.position;
      base.Enter();
    }
    else
    {
      Failed();
    }
   // targetDoor = owner.GetComponent<Player>().Key.KeyData.TargetDoor.gameObject;// .TargetDoor.gameObject;
    //targetDoor = owner.GetComponent<BlackBoard>().TreasureObject;
    //if (!targetDoor)
    //{
    //  Failed();
    //}
    //else
    //{
    //  target = targetDoor.transform.position;
    //  base.Enter();
    //}
  }

  public override void Execute()
  {
    if (InRange)
    {
      //owner.GetComponent<Player>().UseItem("Key");
      //targetDoor.GetComponent<InteractableObject>().Interact(owner);
      key.Unlock(targetDoor.GetComponent<Door>());
      //TODO fixa denna sen.
      targetDoor.GetComponent<InteractableObject>().Interact(owner);
      Successfull();
    }
  }

  public override bool IsInRange()
  {
    InRange = Vector2.Distance(owner.transform.position, targetDoor.transform.position) <= 0.5f;
    return InRange;
  }

}
