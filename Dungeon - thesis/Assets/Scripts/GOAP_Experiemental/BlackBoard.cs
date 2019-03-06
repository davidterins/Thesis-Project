using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Black board.
/// Tanken är att agentens minne ska finnas här i en massa properties.
/// </summary>
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

}
