using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBuilderManager : MonoBehaviour
{
  [SerializeField]
  DungeonImporter DungeonImporter = null;

  [SerializeField]
  Tilemap BaseTileLayer = null;


  [SerializeField]
  UnityEngine.Tilemaps.Tile FloorTile = null;

  [SerializeField]
  UnityEngine.Tilemaps.Tile WallTile = null;

  private DungeonModel dungeon;


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
    dungeon = DungeonImporter.Dungeon;

  }

  private void BuildBaseLayer()
  {
    foreach (Tile tile in dungeon.InitialRoom.Tiles)
    {

        BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(FloorTile));
      if(tile.Type == TileType.WALL)
        BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(WallTile));

    }
    BaseTileLayer.RefreshAllTiles();
  }



  void LateUpdate()
  {
    if (Input.GetKeyDown(KeyCode.B))
    {
      BuildBaseLayer();
    }
  }

}
