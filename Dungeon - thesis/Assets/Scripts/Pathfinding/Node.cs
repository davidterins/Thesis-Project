using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {     // TODO: struct istället
    private int gridX, gridY;
    //public Vector3Int position;
    public Vector3Int position;
    public TileType tileType;
    public Node parent;
    public float gCost, hCost;
    public float fCost { get { return gCost + hCost; } }

    public Node(Vector3Int position, TileType tileType) {
        this.position = position;
        this.tileType = tileType;
    }

    public Vector3 GetFloatPosition() {
        return new Vector3(position.x + 0.5f, position.y + 0.5f, 0f);
    }
}
