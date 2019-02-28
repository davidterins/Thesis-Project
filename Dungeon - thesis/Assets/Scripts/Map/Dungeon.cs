using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The actual game dungeon built from its model.
/// </summary>
public class Dungeon : MonoBehaviour
{
  [SerializeField]
  DungeonImporter DungeonImporter = null;

  [SerializeField]
  Grid worldGrid = null;

  // Start is called before the first frame update
  void Start()
  {
    CreateFromModel(DungeonImporter.Dungeon);
    BuildFirstRoom();
  }

  public int InitialRoomID { get; private set; }

  public Vector2 InitialPos { get; private set; }

  public List<Room> Rooms { get; set; }

  public Dictionary<int, Room> RoomLookup { get; private set; }

  public Room CurrentRoom { get; private set; }

  //Grid can be used to convert world positions to cellpositions and the other way around.
  public Grid WorldGrid { get { return worldGrid; } }

  public void BuildFirstRoom()
  {
    CurrentRoom = RoomLookup[InitialRoomID];
    RoomBuilder.Singleton.BuildRoom(CurrentRoom);
  }

  public void BuildNewRoom(int roomID)
  {
    //RoomBuilder.Singleton.BuildRoom(RoomLookup[InitialRoomID]);
  }

  // Update is called once per frame
  void Update()
  {

  }

  /// <summary>
  /// Acts like the constructor for this class. 
  /// </summary>
  /// <param name="model">Model.</param>
  public void CreateFromModel(DungeonModel model)
  {
    InitialRoomID = model.InitialRoomID;
    InitialPos = model.InitialPos;
    RoomLookup = new Dictionary<int, Room>(model.Rooms.Count);

    foreach (RoomModel roomModel in model.Rooms)
    {
      var room = new Room();
      room.CreateFromModel(roomModel);
      RoomLookup[room.RoomID] = room;
    }
  }
}
