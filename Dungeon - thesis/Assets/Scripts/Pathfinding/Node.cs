using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {     // TODO: struct istället
    private int gridX, gridY;
    public Vector3Int position;
    public TileType tileType;
    public Node parent;
    public int gCost, hCost;
    public int fCost { get { return gCost + hCost; } }

    public Node(Vector3Int position, TileType tileType) {
        this.position = position;
        this.tileType = tileType;
    }
}
