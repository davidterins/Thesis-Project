using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// The actual game dungeon built from its model.
/// </summary>
public class Dungeon : MonoBehaviour
{

  [SerializeField]
  DungeonImporter DungeonImporter = null;

  [SerializeField]
  Grid worldGrid = null;

  private static Dungeon Instance;
  public static Dungeon Singleton { get { return Instance; } }

  void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
  }

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

  Room currentRoom;
  public Room CurrentRoom { get { return currentRoom; } set { currentRoom = value; } }

  //Grid can be used to convert world positions to cellpositions and the other way around.
  public Grid WorldGrid { get { return worldGrid; } }

  public void ChangeRoom(int roomID, Vector2 targetDoorPosition)
  {
    try
    {
      var targetRoom = transform.Find("Room " + roomID).gameObject;
      var prevRoom = transform.Find("Room " + CurrentRoom.RoomID).gameObject;
      prevRoom.SetActive(false);
      CurrentRoom = RoomLookup[roomID];
      RoomBuilder.Singleton.CreateTileLayersOnRoomChange(CurrentRoom);
      targetRoom.SetActive(true);
    }
    catch (System.Exception ex)
    {
      BuildNewRoom(roomID, targetDoorPosition);
    }
  }

  public GameObject GetRoomObject(int roomID)
  {
    return transform.Find("Room " + roomID).gameObject;
  }

  public void BuildFirstRoom()
  {
    CurrentRoom = RoomLookup[InitialRoomID];
    RoomBuilder.Singleton.BuildRoom(CurrentRoom, Vector2.left);
  }

  public void BuildNewRoom(int roomID, Vector2 targetDoorPosition)
  {
    var prevRoom = transform.Find("Room " + CurrentRoom.RoomID).gameObject;
    CurrentRoom = RoomLookup[roomID];
    RoomBuilder.Singleton.BuildRoom(RoomLookup[roomID], targetDoorPosition);
    prevRoom.SetActive(false);
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
