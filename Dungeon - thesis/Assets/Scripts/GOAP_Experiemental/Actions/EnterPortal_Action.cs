using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPortal_Action : MovingAction_Goap
{
  GameObject portal;

  public EnterPortal_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.EnterPortal;

    PreConditions = new WorldStateSymbol[]
    {
      WorldStateSymbol.PortalLocated
     };

    Effects = new WorldStateSymbol[]
    {
      WorldStateSymbol.CanWin,
     };
  }

  public override void Enter()
  {

    portal = owner.GetComponent<BlackBoard>().Memory[typeof(Portal)][0];
    target = portal.transform.position;
    base.Enter();
  
    //else
    //{
    //  Failed();
    //}
  }

  public override void Execute()
  {
    if (InRange)
    {
      //key.Use(targetDoor.GetComponent<Door>());
      //TODO fixa denna sen.
      portal.GetComponent<InteractableObject>().Interact(owner);
      Successfull();
    }
  }


  public override bool IsInRange()
  {
    InRange = Vector2.Distance(owner.transform.position, portal.transform.position) <= 0.5f;
    return InRange;
  }
}
