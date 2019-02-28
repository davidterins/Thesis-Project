using System;
using System.Xml.Serialization;


/// <summary>
/// RoomModel used only for importation 
/// </summary>
[Serializable]
public class RoomModel
{
  public RoomModel() { }

  public int RoomID { get; set; }

  public int Width { get; set; }

  public int Height { get; set; }

  public double EntranceSafety { get; set; }

  [XmlElement(ElementName = "ConnectingRooms")]
  public RoomEdgeModel[] ConnectingRoomIDs { get; set; }

  //Required markup for collections
  [XmlElement(ElementName = "Tiles")]
  public TileModel[] Tiles { get; set; }


  //public Room Room
  //{
  //  get
  //  {
  //    Room room = new Room(); room.CreateFromModel(this); return Room;
  //  }
  //}
}
