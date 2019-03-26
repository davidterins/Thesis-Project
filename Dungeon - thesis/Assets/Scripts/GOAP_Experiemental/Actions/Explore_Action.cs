using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explore_Action : MovingAction_Goap
{
  Vector2 tilePosition = Vector2.zero;

  public Explore_Action(GameObject owner) : base(owner)
  {
    ID = ActionID.Explore;

    PreConditions = new WorldStateSymbol[]
    {
     
     };

    Effects = new WorldStateSymbol[]
    {
      WorldStateSymbol.RoomExplored,
     };
  }

  public override void Enter()
  {
    target = Dungeon.Singleton.CurrentRoom.RoomGraph.GetUnexploredPosition();
    if(target == Vector2.zero)
    {
      Failed();
      return;
    }
    base.Enter();
  }

  public override void Execute()
  {
    Successfull();
  }

  public override bool IsInRange()
  {
    InRange = Vector2.Distance(owner.transform.position, tilePosition) <= 0.5f;
    return InRange;
  }
}
