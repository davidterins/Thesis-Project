﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Door : InteractableObject
{

  public Vector2 TargetDoorPosition { get; set; }

  public int TargetRoomID { get; set; }

  public bool IsOpen { get; set; }

  //private void OnTriggerEnter2D(Collision2D collision)
  //{
  //  if (collision.gameObject.tag == "Player")
  //    DoorEnter(collision.gameObject);
  //}

  void DoorEnter(GameObject player)
  {
    var dungeon = GameObject.FindWithTag("Dungeon").GetComponent<Dungeon>();
    var room = dungeon.RoomLookup[TargetRoomID];
    dungeon.ChangeRoom(TargetRoomID);

    player.transform.position = new Vector2(TargetDoorPosition.x + 0.5f, TargetDoorPosition.y + 0.5f);

  }

  public override void Interact(GameObject player)
  {
    DoorEnter(player);
  }
}
