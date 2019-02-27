using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {
    // Normal Movements Variables
    private float walkSpeed;
    //private float curSpeed;
    //private float maxSpeed;
    private bool hasTarget, interruptPath;
    private bool isBetweenTiles = false;
    private Graph pathGraph;
    private List<Node> path;
    //private RoomModel currentRoom;
    private Tile targetTile;
    private int pathIndex = 0;
    private MapBuilderManager mbm;

    //private CharacterStat plStat;
    
    void Start() {
        pathGraph = GameObject.Find("A*").transform.GetComponent<Graph>();
        //currentRoom = GameObject.Find("MapBuilder").GetComponent<MapBuilderManager>().GetCurrentRoom();
        mbm = GameObject.Find("MapBuilder").GetComponent<MapBuilderManager>();
        hasTarget = false;
        interruptPath = false;
        walkSpeed = 1;
    }

    void FixedUpdate() {
        //curSpeed = walkSpeed;
        //maxSpeed = curSpeed;

        // Acquire path if agent has no target and isn't instructed to change path
        if ((!hasTarget || interruptPath) && !isBetweenTiles) {
            path = pathGraph.FindPath(mbm.GetBaseLayer().GetTile(new Vector3Int((int)transform.position.x, (int)transform.position.y, 0)), targetTile); // FindPath(startTile, targetTile)
            hasTarget = true;
            interruptPath = false;
        }

        // Move a tile
        if (hasTarget && isBetweenTiles)
            transform.position = Vector3.MoveTowards(transform.position, path[pathIndex].position, walkSpeed);
        if (Vector3.Distance(transform.position, path[pathIndex].position) < 0.001f) {
            isBetweenTiles = false;
            pathIndex++;
            if (pathIndex > path.Count) {   // >= ?
                hasTarget = false;
            }
        }

        // Move senteces
        //GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * curSpeed, 0.8f),
        //Mathf.Lerp(0, Input.GetAxis("Vertical") * curSpeed, 0.8f));
    }
}

