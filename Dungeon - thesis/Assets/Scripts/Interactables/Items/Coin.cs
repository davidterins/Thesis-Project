using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coin : Item
{

  [SerializeField]
  public readonly int value = 10;

  public void Start()
  {
    InfoBox.coins += value;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    Player agentInfo = other.gameObject.GetComponent<Player>();
    if (agentInfo)
    {
      agentInfo.HandlePickup(this);
      Destroy(gameObject, 1);
    }
  }
}
