using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEdgeModel { 
  public RoomEdgeModel() { }

  public Vector2 StartDoorPosition { get; set; }
   
  public Vector2 TargetDoorPosition { get; set; }

  public int ToRoomID { get; set; }
}
