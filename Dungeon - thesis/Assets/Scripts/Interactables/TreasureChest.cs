using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
  bool IsClosed = true;

  [SerializeField]
  GameObject[] Loot = null;

  void Start()
  {

  }

  void Open()
  {
    foreach(var item in Loot)
    {
      Instantiate(item, transform);
    }
    IsClosed = false;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (IsClosed)
      if (collision.gameObject.CompareTag("Player"))
      {
        Open();
        Debug.Log(" Player collided with chest");
      }

  }
}
