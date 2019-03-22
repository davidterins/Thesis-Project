using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;

public class TreasureChest : InteractableObject
{
  bool isClosed = true;
  public bool IsClosed { get { return isClosed; } private set { isClosed = value; } }


  [SerializeField]
  Sprite ChestOpen = null;

 

  void Start() { }

  void Open(GameObject interactingAgent)
  {
    var dungeon = GameObject.FindWithTag("Dungeon").GetComponent<Dungeon>();
    var loot = transform.GetChild(0);
    loot.transform.SetParent(dungeon.GetRoomObject(dungeon.CurrentRoom.RoomID).transform);
    loot.GetComponent<Loot>().Drop(interactingAgent, transform.position);


    GetComponent<SpriteRenderer>().sprite = ChestOpen;
    isClosed = false;

    interactingAgent.GetComponent<BlackBoard>().RemovePOI(TileType.TREASURE, gameObject);
    //try
    //{
    //  interactingAgent.GetComponent<BlackBoard>().RemovePOI(TileType.TREASURE, gameObject);
    //  //this.GetComponent<BoxCollider2D>().enabled = false;
    //}
    //catch { Debug.Log("Agent has no memory applied."); }
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
