using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {
    private Transform startPos;
    private Transform targetPos;
    private int distance;
    private Node[,] node;
    private List<Node> finalPath;
    //private int gridSizeX, gridSizeY;

    public Graph(int gridSizeX, int gridSizeY) {
        node = new Node[gridSizeX, gridSizeY];
        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                node[x, y] = new Node(new Vector3Int(gridSizeX, gridSizeY, 0), TileType.WALL);
            }
        }
    }

    public void InsertNode(Vector3Int position, TileType tileType) {
        // Make sure we aren't inserting impassable tiles such as walls
        if (tileType != TileType.WALL) {
            node[position.x, position.y] = new Node(position, tileType);
        }
    }

    public void FindPath(Vector2 startPos, Vector2 targetPos) {
        Node startNode = 
    }
}
