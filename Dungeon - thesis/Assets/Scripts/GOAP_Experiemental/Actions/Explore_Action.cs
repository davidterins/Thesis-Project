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
    target = GameObject.FindWithTag("Dungeon").GetComponent<Dungeon>().CurrentRoom.RoomGraph.GetUnexploredPosition();
    if(target == Vector2.zero)
    {
      Failed();
      return;
    }


    base.Enter();
    //if (!IsInRange())
    //{
    //movement = owner.GetComponent<Movement>();
    // TODO: Call on graph.GetUnexploredPosition() and move to that location.
    //tilePosition = GameObject.FindWithTag("Dungeon").GetComponent<Dungeon>().CurrentRoom.RoomGraph.GetUnexploredPosition();
    //}
  }

  public override void Execute()
  {
    //base.Execute();
    Successfull();
  }

  public override bool IsInRange()
  {
    InRange = Vector2.Distance(owner.transform.position, tilePosition) <= 0.5f;
    return InRange;
  }
}
