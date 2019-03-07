using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{

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

