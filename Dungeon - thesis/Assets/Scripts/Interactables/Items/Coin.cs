using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coin : Item
{

  [SerializeField]
  public readonly int value = 10;

  float destroyDelay = 0;

  public void Start()
  {

    GetComponent<Animator>().SetTrigger("OnPickup");
    destroyDelay = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + destroyDelay;
   // agentInfo.HandlePickup(this);

    Destroy(gameObject, destroyDelay);
  }

  //private void OnTriggerEnter2D(Collider2D other)
  //{
  //  Player agentInfo = other.gameObject.GetComponent<Player>();
  //  if (agentInfo)
  //  {
  //    GetComponent<Animator>().SetTrigger("OnPickup");
  //    destroyDelay = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + destroyDelay;
  //    agentInfo.HandlePickup(this);
     
  //   Destroy(gameObject, destroyDelay);
  //  }
  //}
}
