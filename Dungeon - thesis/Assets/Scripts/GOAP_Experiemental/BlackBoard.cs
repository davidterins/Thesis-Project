using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard : MonoBehaviour
{
  GameObject dungeonObj;
  void Start()
  {
    dungeonObj= GameObject.FindWithTag("Dungeon");
  }

  void Update()
  {

  }

  public int Health { get { return GetComponent<PlayerInfo>().Health; } }

  public int Coins { get { return GetComponent<PlayerInfo>().Coins; } }

  //public int EnemiesInRoom {get {  }


}
