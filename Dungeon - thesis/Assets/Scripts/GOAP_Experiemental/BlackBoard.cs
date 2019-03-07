using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Add hashing to Dictionary List

/// <summary>
/// Black board.
/// Tanken är att agentens minne ska finnas här i en massa properties.
/// </summary>
public class BlackBoard : MonoBehaviour {

    Dictionary<TileType, List<GameObject>> memory;

    public void Start() {
        memory = new Dictionary<TileType, List<GameObject>>() {
            { TileType.ENEMY, new List<GameObject>() },
            { TileType.TREASURE, new List<GameObject>() },
            { TileType.DOOR, new List<GameObject>() },
            { TileType.DOORENTER, new List<GameObject>() }
        };
    }

    public int Health { get { return GetComponent<PlayerInfo>().Health; } }

    public int Coins { get { return GetComponent<PlayerInfo>().Coins; } }

    public void AddPOI(GameObject go, TileType type) {
        try {
            if (!memory[type].Contains(go))
                memory[type].Add(go);
        }
        catch { }   
    }

    public void RemovePOI(GameObject go, TileType type) {
        if (memory[type].Contains(go))
            memory[type].Remove(go);
    }
}
