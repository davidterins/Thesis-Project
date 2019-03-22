using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor_Action : MovingAction_Goap
{

  GameObject targetDoor;

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
    //TODO Behöver få in så att en Key har en referens till end dörr. Nu går den 
    // till en kista:9
    if (!PreconditionsSatisfied())
    {
      Failed();
      return;
    }
   
    targetDoor = owner.GetComponent<Player>().Key.KeyData.TargetDoor.gameObject;// .TargetDoor.gameObject;
    //targetDoor = owner.GetComponent<BlackBoard>().TreasureObject;
    if (!targetDoor)
    {
      Failed();
    }
    else
    {
      target = targetDoor.transform.position;
      base.Enter();
    }
  }

  public override void Execute()
  {
    if (InRange)
    {
      owner.GetComponent<Player>().UseItem("Key");
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
