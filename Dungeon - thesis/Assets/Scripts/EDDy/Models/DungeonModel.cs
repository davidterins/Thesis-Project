using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


// TODO need some kind of worldpos for rooms so that they do not overlap.
// Could be solved by only loading current room.
[Serializable]
public class DungeonModel
{
  public DungeonModel() { }

  public DungeonModel(RoomModel InitialRoom, Vector2 InitialPos, List<RoomModel> Rooms)
  {
    this.InitialPos = InitialPos;
    this.InitialRoom = InitialRoom;
    this.Rooms = Rooms;
  }

  public RoomModel InitialRoom { get; set; }

  public Vector2 InitialPos { get; set; }

  //Required markup for collections
  [XmlElement(ElementName = "Rooms")]
  public List<RoomModel> Rooms { get; set; }
}
