using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Door : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  //Vector2 StartDoorPosition { get; set; }

  public Vector2 TargetDoorPosition { get; set; }

  public int TargetRoomID { get; set; }


  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Player")
      DoorEnter(collision.gameObject);
  }

  void DoorEnter(GameObject player)
  {

   // var room = RoomBuilder.Singleton.DungeonRoomLookup[TargetRoomID];
    //var tile = room.Tiles.Where(o => (int)o.Position.x == (int)TargetDoorPosition.x && (int)o.Position.y == (int)TargetDoorPosition.y).FirstOrDefault();
    //Att göra async sen
   // RoomBuilder.Singleton.BuildRoom(TargetRoomID);
    Debug.Log("Collided with Exit Door");
  }
}
