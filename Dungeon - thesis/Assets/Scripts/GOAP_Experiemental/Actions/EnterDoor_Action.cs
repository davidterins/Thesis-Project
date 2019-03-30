using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDoor_Action : MovingAction_Goap
{

  GameObject targetDoor;
  Key key;

  public EnterDoor_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.OpenDoor;

    PreConditions = new WorldStateSymbol[]
   {
      
   };

    Effects = new WorldStateSymbol[]
    {

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
  }

  public override void Execute()
  {
    if (InRange)
    {
      key.Use(targetDoor.GetComponent<Door>());
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
