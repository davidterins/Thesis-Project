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

  //Required markup for collections
  [XmlElement(ElementName = "Tiles")]
  public Tile[] Tiles { get; set; }

  public double EntranceSafety { get; set; }
}
