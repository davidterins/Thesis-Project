using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.Experimental.PlayerLoop;
using System.Linq;

public class MapBuilderManager : MonoBehaviour {
    [SerializeField]
    DungeonImporter DungeonImporter = null;

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

    //[SerializeField]
    //RuleTile RuleTileTest = null;

    [SerializeField]
    GameObject player = null;

    [SerializeField]
    GameObject Enemy = null;

    [SerializeField]
    GameObject TreasureChest = null;

    [SerializeField]
    GameObject ExitDoor = null;

    public DungeonModel DungeonModel { get; private set; }

    private Graph roomGraph;

    private RoomModel currentRoom;
    public RoomModel GetCurrentRoom() { return currentRoom; }
    public Tilemap GetBaseLayer() { return BaseTileLayer; }

    public Dictionary<int, RoomModel> DungeonRoomLookup { get; private set; }

    private static MapBuilderManager Instance;
    public static MapBuilderManager Singleton { get { return Instance; } }

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        //Gets the imported map
        DungeonModel = DungeonImporter.Dungeon;

        DungeonRoomLookup = new Dictionary<int, RoomModel>();
        foreach (RoomModel room in DungeonModel.Rooms) {
            DungeonRoomLookup[room.RoomID] = room;
        }

        BuildBaseLayer(DungeonModel.InitialRoomID);

    }

    public void BuildRoom(int roomID) {
        BuildBaseLayer(roomID);
    }

    // TODO Här ska den bygga arrayn för pathfinding också sen
    private void BuildBaseLayer(int roomID) {
        //För att rensa när rum två laddas in, görs på annat sätt sen. 
        BaseTileLayer.ClearAllTiles();
        CollideLayer.ClearAllTiles();

        RoomModel room = DungeonRoomLookup[roomID];
        roomGraph = new Graph(room.Width, room.Height);
        currentRoom = room;

        foreach (Tile tile in room.Tiles) {
            Vector3Int tilePos = new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0);
            if (tile.Type != TileType.WALL)
                roomGraph.InsertNode(tilePos, tile.Type);

            var centerOfTile = BaseTileLayer.GetCellCenterWorld(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0));
            switch (tile.Type) {

                case TileType.FLOOR:

                    BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(FloorTile));
                    break;
                case TileType.WALL:

                    CollideLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(WallTile));

                    break;
                case TileType.TREASURE:
                    BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(FloorTile));

                    var center = BaseTileLayer.GetCellCenterWorld(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0));
                    Instantiate(TreasureChest, center, Quaternion.identity);

                    break;
                case TileType.ENEMY:

                    BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(FloorTile));
                    Instantiate(Enemy, centerOfTile, Quaternion.identity);

                    break;
                case TileType.DOOR:

                    BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(DoorTile));
                    var doorObj = Instantiate(ExitDoor, centerOfTile, Quaternion.identity);
                    var doorScript = doorObj.GetComponent<Door>();

                    //TODO Ta bort linq function när det finns något bättre sätt att kolla upp detta...
                    RoomEdgeModel roomConnection = room.ConnectingRoomIDs.Where(o => (int)o.StartDoorPosition.x == tilePos.x && (int)o.StartDoorPosition.y == tilePos.y).First();

                    doorScript.TargetRoomID = roomConnection.ToRoomID;
                    doorScript.TargetDoorPosition = roomConnection.TargetDoorPosition;

                    break;
                case TileType.DOORENTER:

                    //BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(DoorEnterTile));
                    BaseTileLayer.SetTile(tilePos, Instantiate(DoorEnterTile));
                    Camera.main.GetComponent<SmoothCamera>().target = Instantiate(player, centerOfTile, Quaternion.identity);
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
