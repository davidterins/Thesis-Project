using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : InteractableObject, IMemorizable
{

  public Type MemorizableType { get { return GetType(); } }

  bool ofInterest = true;
  public bool OfInterest { get { return ofInterest; } set { ofInterest = value; } }

  //private void OnTriggerEnter2D(Collision2D collision)
  //{
  //  if (collision.gameObject.tag == "Player")
  //    DoorEnter(collision.gameObject);
  //}


  void Enter(GameObject player)
  {
    //TODO WIN stuff
  }

  public override void Interact(GameObject player)
  {
    Enter(player);
  }
}

