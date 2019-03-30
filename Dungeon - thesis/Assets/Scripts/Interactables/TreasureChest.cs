using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;

public class TreasureChest : InteractableObject, IMemorizable
{
  bool isClosed = true;
  public bool IsClosed { get { return isClosed; } private set { isClosed = value; } }

  public Type MemorizableType { get { return GetType(); } }

  bool ofInterest = true;
  public bool OfInterest { get { return ofInterest; } set { ofInterest = value; } }

  [SerializeField]
  Sprite ChestOpen = null;

 
  void Start() { }

  void Open(GameObject interactingAgent)
  {
    var s = RoomID;
    var dungeon = Dungeon.Singleton;
    var loot = transform.GetChild(0);
    loot.transform.SetParent(dungeon.GetRoomObject(dungeon.CurrentRoom.RoomID).transform);
    loot.GetComponent<Loot>().Drop(interactingAgent, transform.position);
    GetComponent<SpriteRenderer>().sprite = ChestOpen;

    isClosed = false;

    //interactingAgent.GetComponent<BlackBoard>().RemovePOI(TileType.TREASURE, gameObject);
    interactingAgent.GetComponent<BlackBoard>().RemoveTypePOI(GetType(), gameObject);

  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    //if (IsClosed)
    //if (collision.gameObject.CompareTag("Player"))
    //{
    //  Open(collision.gameObject);
    //  Debug.Log(" Player collided with chest");
    //}
  }

  public override void Interact(GameObject player)
  {
    //DebugColoring();
    if (isClosed)
    {
      Open(player);
      Debug.Log(" Player requested to open chest");
    }
  }
}
