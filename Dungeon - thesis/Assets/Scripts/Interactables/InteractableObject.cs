using System;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{

  private void Awake()
  {
    RoomID = Dungeon.Singleton.CurrentRoom.RoomID;
  }

  public int RoomID { get; set; }

  // Min tanke här är att spelaren ska requesta interact genom sina actions,
  // istället för att forca en interaction med en trigger. Tror det blir lättare
  // att kontrollera såhär.
  public abstract void Interact(GameObject player);

  protected void DebugColoring()
  {
    GetComponent<SpriteRenderer>().color = Color.green;
  }
}

