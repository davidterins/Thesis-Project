using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Add hashing to Dictionary List
using System.Linq;

/// <summary>
/// Black board.
/// Tanken är att agentens minne ska finnas här i en massa properties.
/// </summary>
public class BlackBoard : MonoBehaviour
{

  Dictionary<TileType, List<GameObject>> memory;

  public void Start()
  {
    memory = new Dictionary<TileType, List<GameObject>>() {
            { TileType.ENEMY, new List<GameObject>() },
            { TileType.TREASURE, new List<GameObject>() },
            { TileType.DOOR, new List<GameObject>() },
            { TileType.DOORENTER, new List<GameObject>() }
        };
  }

  public void AddPOI(TileType type, GameObject go)
  {
    if (type != TileType.FLOOR)
      try
      {
        if (!memory[type].Contains(go))
        {
          memory[type].Add(go);
          Debug.Log("Added " + type + " count: " + memory[type].Count);
        }

      }
      catch (System.Exception ex)
      {
        Debug.Log(ex.Message + "Type = " + type);
      }

  }

  public void RemovePOI(TileType type, GameObject go)
  {
    if (memory[type].Contains(go))
      memory[type].Remove(go);
  }

  public GameObject AttackTarget
  {
    get
    {
      var s = memory[TileType.ENEMY].Where(o => o != null).First();
      return s;
    }
  }

  public int Health { get { return GetComponent<Player>().Health; } }

  public int Coins { get { return GetComponent<Player>().Coins; } }

}
