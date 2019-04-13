using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explore_Action : MovingAction_Goap
{

  public Explore_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.Explore;

    PreConditions = new WorldStateSymbol[]
    {
      WorldStateSymbol.RoomUnexplored
     };

    Effects = new WorldStateSymbol[]
    {
      WorldStateSymbol.RoomExplored,
      WorldStateSymbol.AvailableChest,
      WorldStateSymbol.AvailableEnemy,
     };
    //interactionRange = 0.5f;
  }

  public override void Enter()
  {
    target = Dungeon.Singleton.CurrentRoom.RoomGraph.GetUnexploredPosition(owner.transform.position);
    if (target == Vector2.zero)
    {
      Failed();
      return;
    }
    base.Enter();
  }

  public override void Execute()
  {
    if (InRange)
    {
      Successfull();
    }

  }

  public override bool IsInRange()
  {
    float distance = Vector2.Distance(owner.transform.position, target);
    InRange = distance <=  0.8f;
    return InRange;
  }
}
