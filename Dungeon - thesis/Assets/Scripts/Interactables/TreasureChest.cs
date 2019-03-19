using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : InteractableObject, ILootableObject
{
  bool isClosed = true;
  public bool IsClosed { get { return isClosed; } private set { isClosed = value; } }

  [SerializeField]
  Sprite ChestOpen = null;

  [SerializeField]
  List<GameObject> Loot = null;

  void Start() { }

  void Open(GameObject interactingAgent)
  {
    //interactingAgent.GetComponent<BlackBoard>().TargetLoot.Clear();
    var tempLootList = new List<GameObject>(Loot.Count);
    foreach (var item in Loot)
    {
      float dropRateValue = Random.Range(0.00f, 1.00f);

      if (dropRateValue <= item.GetComponent<Item>().GetDropRate())
      {
        var lootObj = Instantiate(item, transform.position, Quaternion.identity);
        tempLootList.Add(lootObj);
       
      }
      if (item.GetComponent<Key>())
      {
        var lootObj = Instantiate(item, transform.position, Quaternion.identity);
        interactingAgent.GetComponent<BlackBoard>().ImportantItemDrop = lootObj;
        tempLootList.Add(lootObj);
      }
    }
    if (tempLootList.Count > 0)
      interactingAgent.GetComponent<BlackBoard>().TargetLoot = tempLootList;


    GetComponent<SpriteRenderer>().sprite = ChestOpen;
    isClosed = false;
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

  public void AddLoot(GameObject gameObject)
  {
    DebugColoring();
    Loot.Add(gameObject);
  }
}
