using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Add hashing to Dictionary List
using System;

/// <summary>
/// Black board.
/// Tanken är att agentens minne ska finnas här i en massa properties.
/// </summary>
public class BlackBoard : MonoBehaviour {
    public event EventHandler<WsSymbolChangedEventArgs> WorldStateVariableChanged;

    TargetingService targetingService;
    //[SerializeField]
    //Persona currentPersona;

    public Dictionary<TileType, List<GameObject>> Memory { get; private set; }

    public void Start() {
        //TargetLoot = new List<GameObject>();
        targetingService = new TargetingService(gameObject);
        InvokeRepeating("UpdateTargets", 0, 1.0f);
        //if (!currentPersona)
        //gameObject.AddComponent<DefaultPersona>();

        Memory = new Dictionary<TileType, List<GameObject>>() {
            { TileType.ENEMY, new List<GameObject>() },
            { TileType.TREASURE, new List<GameObject>() },
            { TileType.DOOR, new List<GameObject>() },
            { TileType.DOORENTER, new List<GameObject>() }
        };
    }

    public void AddPOI(TileType type, GameObject go) {
        if (type != TileType.FLOOR)
            try {
                if (!Memory[type].Contains(go)) {
                    Memory[type].Add(go);
                    InfoBox.UpdateMemory(Memory);
                    // Debug.Log("Added " + type + " count: " + Memory[type].Count);
                }
            }
            catch (System.Exception ex) {
                Debug.Log(ex.Message + "Type = " + type);
            }
    }

    public void RemovePOI(TileType type, GameObject go) {
        if (Memory[type].Contains(go)) {
            Memory[type].Remove(go);
            InfoBox.UpdateMemory(Memory);
        }
    }

    private GameObject enemyObject;
    public GameObject EnemyObject {
        get {
            return enemyObject;
        }
        set {
            if (enemyObject != value) {
                enemyObject = value;
                bool wsValue = false;
                if (value != null)
                    wsValue = true;

                WorldStateVariableChanged.Invoke(this, new WsSymbolChangedEventArgs(WorldStateSymbol.AvailableEnemy, wsValue));
            }

        }
    }

    private GameObject treasureTarget;
    public GameObject TreasureObject {
        get { return treasureTarget; }
        set {
            //if (!treasureTarget == value)
            //{
            treasureTarget = value;
            bool wsValue = false;
            if (value != null)
                wsValue = true;

            //Debug.Log("Closest chest is at " + value.transform.position.x + ", " + value.transform.position.y);
            WorldStateVariableChanged.Invoke(this, new WsSymbolChangedEventArgs(WorldStateSymbol.AvailableChest, wsValue));
            //}
        }
    }

    private List<GameObject> targetLoot;
    public List<GameObject> TargetLoot {
        get { return targetLoot; }
        set {
            var s = value;
            if (targetLoot != value) {
                targetLoot = value;
                bool wsValue = false;
                if (value != null)
                    wsValue = true;

                WorldStateVariableChanged.Invoke(this, new WsSymbolChangedEventArgs(WorldStateSymbol.LootableItem, wsValue));
            }
        }
    }

    private GameObject importantItemDrop;
    public GameObject ImportantItemDrop {
        get { return importantItemDrop; }
        set {
            var s = value;
            if (importantItemDrop != value) {
                importantItemDrop = value;
                bool wsValue = false;
                if (value != null)
                    wsValue = true;
                WorldStateVariableChanged.Invoke(this, new WsSymbolChangedEventArgs(WorldStateSymbol.ImportantLoot, wsValue));
            }
        }
    }

    bool hasKey;
    public bool HasKey {
        get { return hasKey; }
        set {
            var s = value;
            if (hasKey != value) {
                hasKey = value;

                WorldStateVariableChanged.Invoke(this, new WsSymbolChangedEventArgs(WorldStateSymbol.HasKey, hasKey));
            }
        }
    }


    public int Health { get { return GetComponent<Player>().Health; } }

    public int Coins { get { return GetComponent<Player>().Coins; } }

    /// <summary>
    /// Updated every second by the InvokeRepeating function in Start().
    /// </summary>
    public void UpdateTargets() { targetingService.Refresh(); }
}

public class WsSymbolChangedEventArgs : EventArgs {
    public WorldStateSymbol Symbol { get; private set; }
    public bool Value { get; private set; }

    public WsSymbolChangedEventArgs(WorldStateSymbol symbol, bool value) {
        Symbol = symbol;
        Value = value;
    }
}
