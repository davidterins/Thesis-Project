using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
  // Normal Movements Variables
  private float walkSpeed;
  private bool hasTarget, interruptPath;
  private bool isBetweenTiles = false;
  //private Graph pathGraph;
  private List<Node> path;
  //private RoomModel currentRoom;
  private TileModel targetTile;
  private int pathIndex = 0;

  void Start()
  {
    //pathGraph = GameObject.Find("A*").transform.GetComponent<Graph>();
    hasTarget = false;
    interruptPath = false;
    walkSpeed = 1;
  }

  void FixedUpdate()
  {

    //if (Input.GetKeyDown(KeyCode.L))
    //{
    // //var dungeon = GameObject.FindWithTag("Dungeon").GetComponent<Dungeon>();
    //  var room = dungeon.CurrentRoom;

    //  var cellPos = dungeon.WorldGrid.WorldToCell(transform.position);

    //  path = room.RoomGraph.FindPath(room.Tiles2D[cellPos.y, cellPos.x], room.Tiles2D[1, 4]);

    //  if (path != null)
    //  {
    //    Debug.Log("Path Count " + path.Count);
    //  }
    //  else
    //    Debug.Log("Path null");
    //}

    //if (Input.GetKeyDown(KeyCode.K))
    //{
    //  var worldTargetPos = dungeon.WorldGrid.CellToWorld(path[pathIndex++].position);e
    //  transform.position = Vector3.MoveTowards(transform.position, worldTargetPos, walkSpeed);
    //}

    //Får vara här så länge för att nå dungeon.
    var dungeon = GameObject.FindWithTag("Dungeon").GetComponent<Dungeon>();
    // Acquire path if agent has no target and isn't instructed to change path
    if ((!hasTarget || interruptPath) && !isBetweenTiles)
    {
      //var dungeon = GameObject.FindWithTag("Dungeon").GetComponent<Dungeon>();
      var room = dungeon.CurrentRoom;
      var cellPos = dungeon.WorldGrid.WorldToCell(transform.position);

      path = room.RoomGraph.FindPath(room.Tiles2D[cellPos.y, cellPos.x], room.Tiles2D[1, 3]);

      hasTarget = true;
      interruptPath = false;
    }

    //if (hasTarget && isBetweenTiles)
    //{
    // Targetpos måste göras om till worldspace samt ändra i node så att target pos är
    // center av en node. Kanske att man skapar en worldPos variabel i Noden direkt så slipper vi göra det hela tiden.
    // var worldTargetPos = dungeon.WorldGrid.CellToWorld(path[pathIndex++].position)
    //  transform.position = Vector3.MoveTowards(transform.position, path[pathIndex].position, walkSpeed);

    //}

    //if (Vector3.Distance(transform.position, path[pathIndex].position) < 0.001f)
    //{
    //  isBetweenTiles = false;
    //  pathIndex++;
    //  if (pathIndex > path.Count)
    //  {   // >= ?
    //    hasTarget = false;
    //  }
    //}


    // Move senteces
    //GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * curSpeed, 0.8f),
    //Mathf.Lerp(0, Input.GetAxis("Vertical") * curSpeed, 0.8f));
  }
}

