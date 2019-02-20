﻿using System.Collections;
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
  Tilemap CollideLayer = null;


  [SerializeField]
  UnityEngine.Tilemaps.Tile FloorTile = null;

  [SerializeField]
  UnityEngine.Tilemaps.Tile WallTile = null;

  [SerializeField]
  UnityEngine.Tilemaps.Tile EnemyTile = null;

  [SerializeField]
  UnityEngine.Tilemaps.Tile DoorTile = null;

  [SerializeField]
  UnityEngine.Tilemaps.Tile DoorEnterTile = null;

  [SerializeField]
  UnityEngine.Tilemaps.Tile TreasureTile = null;

  [SerializeField]
  GameObject player = null;

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

    BuildBaseLayer();

  }

  private void BuildBaseLayer()
  {
    foreach (Tile tile in dungeon.InitialRoom.Tiles)
    {


      switch (tile.Type)
      {
        case TileType.FLOOR:
          BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(FloorTile));
          break;
        case TileType.WALL:
          CollideLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(WallTile));
          break;
        case TileType.TREASURE:
          BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(TreasureTile));
          break;
        case TileType.ENEMY:
          BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(EnemyTile));
          break;
        case TileType.DOOR:
          BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(DoorTile));
          break;
        case TileType.DOORENTER:
          var doorTile = Instantiate(DoorTile);
          BaseTileLayer.SetTile(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0), Instantiate(DoorEnterTile));
          var s = BaseTileLayer.GetCellCenterWorld(new Vector3Int((int)tile.Position.x, (int)tile.Position.y, 0));
          Camera.main.GetComponent<SmoothCamera>().target =Instantiate(player, s , Quaternion.identity);
          break;
        case TileType.NONE:
          break;
        default:
          break;
      }
    }

    BaseTileLayer.RefreshAllTiles();
  }



  void LateUpdate()
  {
    //if (Input.GetKeyDown(KeyCode.B))
    //{
    //  BuildBaseLayer();
    //}
  }

}
