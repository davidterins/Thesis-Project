using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{     // TODO: struct istället
  private int gridX, gridY;
  //public bool explored;
  public Vector3Int position;
  public TileType tileType;
  public Node parent;
  public float gCost, hCost;
  public float fCost { get { return gCost + hCost; } }

  public Node(float gCost)
  {
    this.gCost = gCost;
    position = new Vector3Int(int.MaxValue, int.MaxValue, int.MaxValue);
  }

  public Node(Vector3Int position, TileType tileType)
  {
    this.position = position;
    this.tileType = tileType;
    //explored = false;
  }

  public Vector3 GetFloatPosition()
  {
    return new Vector3(position.x + 0.5f, position.y + 0.5f, 0f);
  }
}
