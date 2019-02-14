using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class DungeonModel
{

  public DungeonModel() { }

  //[XmlElement]
  public RoomModel InitialRoom { get; set; }

  //[XmlElement]
  public Vector2 InitialPos { get; set; }

  //[XmlElement]
  public List<RoomModel> Rooms { get; set; }
}
