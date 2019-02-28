using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


/// <summary>
/// Dungeon used only for importation 
/// </summary>
[Serializable]
public class DungeonModel
{
  public DungeonModel() { }

  public int InitialRoomID { get; set; }

  public Vector2 InitialPos { get; set; }

  //Required markup for collections
  [XmlElement(ElementName = "Rooms")]
  public List<RoomModel> Rooms { get; set; }
}
