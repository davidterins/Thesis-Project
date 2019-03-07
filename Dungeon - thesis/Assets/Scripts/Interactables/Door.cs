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

  public Vector2 TargetDoorPosition { get; set; }

  public int TargetRoomID { get; set; }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Player")
      DoorEnter(collision.gameObject);
  }

  void DoorEnter(GameObject player)
  {
    GameObject.FindWithTag("Dungeon").GetComponent<Dungeon>().BuildNewRoom(TargetRoomID);

    Debug.Log("Collided with Exit Door");
  }
}
