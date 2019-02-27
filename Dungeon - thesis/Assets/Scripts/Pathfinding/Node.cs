using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    private int gridX, gridY;
    private Vector3Int position;
    private TileType tileType;
    private Node parent;
    private float gWeight, hWeight;
    public float fWeight { get { return gWeight + hWeight; } }

    public Node(Vector3Int position, TileType tileType) {
        this.position = position;
        this.tileType = tileType;
    }
}
