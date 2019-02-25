using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.Experimental.PlayerLoop;

public class MapBuilderManager : MonoBehaviour
{
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

  [SerializeField]
  RuleTile RuleTileTest = null;

  [SerializeField]
  GameObject player = null;

  [SerializeField]
  GameObject Enemy = null;

  [SerializeField]
  GameObject TreasureChest = null;

  [SerializeField]
  GameObject ExitDoor = null;

  private DungeonModel importedDungeonModel;

  private static MapBuilderManager Instance;
  public static MapBuilderManager Singleton { get { return Instance; } }

  void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;

    //Gets the imported map
    importedDungeonModel = DungeonImporter.Dungeon;

    BuildBaseLayer();

  }

  // TODO Här ska den bygga arrayn för pathfinding också sen
  private void BuildBaseLayer()
  {
    int i = 0;
    foreach (Tile tile in importedDungeonModel.InitialRoom.Tiles)
    {
     
      var centerOfTile = BaseTileLayer.GetCellCenterWorld(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0));
      switch (tile.Type)
      {
      case TileType.FLOOR:

        BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(FloorTile));
        //RuleTile ruleTile = Instantiate(FloorRuleTile);
        //BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), ruleTile);
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
        Instantiate(ExitDoor, centerOfTile, Quaternion.identity);
        break;
      case TileType.DOORENTER:

        BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(DoorEnterTile));
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
