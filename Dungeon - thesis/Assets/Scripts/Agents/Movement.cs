using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour {
    // Normal Movements Variables
    private float walkSpeed;
    private bool hasTarget, interruptPath;
    private List<Node> path;
    private TileModel targetTile;
    private int pathIndex = 4;
    private float sightRange = 4.5f;
    private Vector3 offset;
    private float scanRotation = 10f;

    public bool PathInterrupted { get { return interruptPath; } }
    public bool HasTarget { get { return hasTarget; } }

    void Start() {
        hasTarget = false;
        interruptPath = false;
        walkSpeed = 0.08f;
        offset = new Vector3(-0.5f, -0.5f, 0.0f);
    }


    /// <summary>
    /// Tillfälligt för att sätta en random tile som target så att den kan användas 
    /// i GotoAction.
    /// </summary>
    public void MoveToTarget() {
        var dungeon = GameObject.FindWithTag("Dungeon").GetComponent<Dungeon>();

        List<TileModel> possibleTiles = new List<TileModel>(dungeon.CurrentRoom.Tiles2D.Length);
        foreach (TileModel t in dungeon.CurrentRoom.Tiles2D) {
            if (t.Type != TileType.WALL)
                possibleTiles.Add(t);
        }
        int randomTileIndex = Random.Range(0, possibleTiles.Count);
        Vector2 target = possibleTiles[randomTileIndex].Position;

        // Acquire path if agent has no target and isn't instructed to change path
        if (!hasTarget || interruptPath) {
            var room = dungeon.CurrentRoom;
            var cellPos = dungeon.WorldGrid.WorldToCell(transform.position);

            path = room.RoomGraph.FindPath(room.Tiles2D[cellPos.y, cellPos.x], room.Tiles2D[(int)target.x, (int)target.y]);
            pathIndex = 0;
            if (path != null && path.Count <= 0)
                hasTarget = false;
            else
                hasTarget = true;
            interruptPath = false;
        }

    }

    void TemporaryWalkOnMouseClick() {
        if (Input.GetMouseButtonDown(0)) {
            interruptPath = true;

            var dungeon = GameObject.FindWithTag("Dungeon").GetComponent<Dungeon>();
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

            var mousePos = dungeon.WorldGrid.WorldToCell(new Vector3((int)pos.x, (int)pos.y, 0));
            var tile = dungeon.CurrentRoom.Tiles2D[mousePos.x, mousePos.y];
            if (tile != null) {
                //Debug.Log(string.Format("Tile is: {0}", tile.Type));
                // Acquire path if agent has no target and isn't instructed to change path
                if (!hasTarget || interruptPath) {
                    var room = dungeon.CurrentRoom;
                    var cellPos = dungeon.WorldGrid.WorldToCell(transform.position);

                    path = room.RoomGraph.FindPath(room.Tiles2D[cellPos.y, cellPos.x], room.Tiles2D[mousePos.y, mousePos.x]);
                    pathIndex = 0;
                    if (path.Count <= 0)
                        hasTarget = false;
                    else
                        hasTarget = true;
                    interruptPath = false;
                }
            }
        }
    }

    private void Update() {
        TemporaryWalkOnMouseClick();

        if (hasTarget && !interruptPath) {
            transform.position = Vector3.MoveTowards(transform.position, path[pathIndex].GetFloatPosition(), walkSpeed);
        }

        if (hasTarget && !interruptPath && (Vector3.Distance(transform.position, path[pathIndex].GetFloatPosition()) < 0.001f)) {
            pathIndex++;
            if (pathIndex >= path.Count) {
                hasTarget = false;
            }
            GetComponent<Vision>().Scan();
        }

        if (hasTarget) {
            InfoBox.pathLength = path.Count;
            InfoBox.playerTile = transform.position.ToString();
            InfoBox.targetTile = path[pathIndex].position.ToString();
            InfoBox.stepsLeft = path.Count - pathIndex;
        }
    }

    private void Interact(TileModel tile) {

    }
}

