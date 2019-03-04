using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.Experimental.PlayerLoop;
using System.Linq;

public class RoomBuilder : MonoBehaviour
{
  [SerializeField]
  Tilemap BaseTileLayer = null;

  [SerializeField]
  Tilemap CollideLayer = null;

  [SerializeField]
  UnityEngine.Tilemaps.Tile FloorTile = null;

  [SerializeField]
  UnityEngine.Tilemaps.Tile WallTile = null;

  [SerializeField]
  UnityEngine.Tilemaps.Tile DoorTile = null;

  [SerializeField]
  UnityEngine.Tilemaps.Tile DoorEnterTile = null;

  [SerializeField]
  GameObject player = null;

  [SerializeField]
  GameObject Enemy = null;

  [SerializeField]
  GameObject TreasureChest = null;

  [SerializeField]
  GameObject ExitDoor = null;

  [SerializeField]
  GameObject Room = null;

  public Tilemap GetBaseLayer() { return BaseTileLayer; }

  private static RoomBuilder Instance;
  public static RoomBuilder Singleton { get { return Instance; } }

  void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
  }

  public void BuildRoom(Room room)
  {
    BuildTileMapLayers(room);
  }

  private void BuildTileMapLayers(Room roomToBuild)
  {
    //För att rensa när rum två laddas in, görs på annat sätt sen. 
    BaseTileLayer.ClearAllTiles();
    CollideLayer.ClearAllTiles();

    Dungeon dungeon = GameObject.FindWithTag("Dungeon").GetComponent<Dungeon>();

    //Build the room gameObject
    GameObject roomObj = Instantiate(Room, dungeon.gameObject.transform);
    roomObj.name = "Room " + roomToBuild.RoomID;

    //Create its room script and set some variables.
    Room room = roomObj.GetComponent<Room>();
    room = roomToBuild;
    room.RoomGraph = new Graph(room.Width, room.Height);


    foreach (TileModel tile in room.Tiles2D)
    {

      Vector3Int tilePos = new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0);

      room.RoomGraph.InsertNode(tilePos, tile.Type);

      var centerOfTile = BaseTileLayer.GetCellCenterWorld(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0));
      switch (tile.Type)
      {
        case TileType.FLOOR:

          BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(FloorTile));
          break;
        case TileType.WALL:

          BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(FloorTile));
          CollideLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(WallTile));

          break;
        case TileType.TREASURE:
          BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(FloorTile));

          var center = BaseTileLayer.GetCellCenterWorld(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0));
          Instantiate(TreasureChest, center, Quaternion.identity, roomObj.transform);

          break;
        case TileType.ENEMY:

          BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(FloorTile));
          Instantiate(Enemy, centerOfTile, Quaternion.identity, roomObj.transform);

          break;
        case TileType.DOOR:

          BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(DoorTile));
          var doorObj = Instantiate(ExitDoor, centerOfTile, Quaternion.identity, roomObj.transform);
          var doorScript = doorObj.GetComponent<Door>();

          RoomEdgeModel roomConnection = dungeon.RoomLookup[room.RoomID].ConnectionLookup[new Vector2(tilePos.x, tilePos.y)];
          doorScript.TargetRoomID = roomConnection.ToRoomID;
          doorScript.TargetDoorPosition = roomConnection.TargetDoorPosition;

          break;
        case TileType.DOORENTER:
        
          BaseTileLayer.SetTile(tilePos, Instantiate(DoorEnterTile));
          Camera.main.GetComponent<SmoothCamera>().target = Instantiate(player, centerOfTile, Quaternion.identity, roomObj.transform);
          break;
        case TileType.NONE:
          break;
        default:
          break;
      }
      BaseTileLayer.RefreshAllTiles();
    }
  }
}
