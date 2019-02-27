using System;
using System.Xml.Serialization;


// TODO get values to which room a door is connected to.
// Also get patterns for the room.
// Add a world pos
[Serializable]
public class RoomModel
{
  public RoomModel(){}

  public RoomModel(Tile[] Tiles, double EntranceSafety)
  {
    this.Tiles = Tiles;
    this.EntranceSafety = EntranceSafety;
  }


  [XmlElement(ElementName = "ConnectingRooms")]
  public RoomEdgeModel[] ConnectingRoomIDs { get; set; }

  //Required markup for collections
  [XmlElement(ElementName = "Tiles")]
  public Tile[] Tiles { get; set; }

  public int RoomID { get; set; }

  public int Width { get; set; }

  public int Height { get; set; }

  public double EntranceSafety { get; set; }
}
