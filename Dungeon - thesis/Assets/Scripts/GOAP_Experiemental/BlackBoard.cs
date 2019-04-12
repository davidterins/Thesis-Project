using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Add hashing to Dictionary List
using System;

/// <summary>
/// Black board.
/// Tanken är att agentens minne ska finnas här i en massa properties.
/// </summary>
public class BlackBoard : MonoBehaviour
{
  public event EventHandler<WsSymbolChangedEventArgs> WorldStateVariableChanged;

  TargetingService targetingService;

  public Dictionary<Type, List<GameObject>> Memory { get; private set; }
  Dictionary<WorldStateSymbol, InventoryItemStatus> HasItemLookup;

  public void Start()
  {
    targetingService = new TargetingService(gameObject);
    InvokeRepeating("UpdateTargets", 0, 1.0f);

    HasItemLookup = new Dictionary<WorldStateSymbol, InventoryItemStatus>();
    Memory = new Dictionary<Type, List<GameObject>>();
  }

  public void AddTypePOI(Type type, GameObject go)
  {
    if (!Memory.ContainsKey(type))
    {
      Memory[type] = new List<GameObject>();
      //Debug.Log("BLACKBOARD ADDED TYPE: " + type);
    }
    Memory[type].Add(go);
    InfoBox.UpdateMemory(Memory);
    if (type == typeof(Portal))
    {
      WorldStateVariableChanged.Invoke(this, new WsSymbolChangedEventArgs(WorldStateSymbol.PortalLocated, true));
    }
  }

  public void RemoveTypePOI(Type type, GameObject go)
  {
    if (Memory.ContainsKey(type))
    {
      Memory[type].Remove(go);
      //Debug.Log("REMOVED FROM TYPE: " + type);
    }
    InfoBox.UpdateMemory(Memory);
  }

  private GameObject targetEnemyObject;
  public GameObject TargetEnemyObject
  {
    get
    {
      return targetEnemyObject;
    }
    set
    {
      //if (enemyObject != value) {
      targetEnemyObject = value;
      bool wsValue = false;
      if (value != null)
        wsValue = true;

      WorldStateVariableChanged.Invoke(this, new WsSymbolChangedEventArgs(WorldStateSymbol.AvailableEnemy, wsValue));
    }
  }

  private GameObject targetTreasureChest;
  public GameObject TargetTreasureChest
  {
    get { return targetTreasureChest; }
    set
    {
      //if (!treasureTarget == value)
      //{
      targetTreasureChest = value;
      bool wsValue = false;
      if (value != null)
        wsValue = true;

      WorldStateVariableChanged.Invoke(this, new WsSymbolChangedEventArgs(WorldStateSymbol.AvailableChest, wsValue));
    }
  }

  private List<GameObject> targetLoot;
  public List<GameObject> TargetLoot
  {
    get { return targetLoot; }
    set
    {
      var s = value;
      if (targetLoot != value)
      {
        targetLoot = value;
        bool wsValue = false;
        if (value != null)
          wsValue = true;

        WorldStateVariableChanged.Invoke(this, new WsSymbolChangedEventArgs(WorldStateSymbol.LootableItem, wsValue));
      }
    }
  }

  private GameObject importantItemDrop;
  public GameObject ImportantItemDrop
  {
    get { return importantItemDrop; }
    set
    {
      var s = value;
      if (importantItemDrop != value)
      {
        importantItemDrop = value;
        bool wsValue = false;
        if (value != null)
          wsValue = true;
        WorldStateVariableChanged.Invoke(this, new WsSymbolChangedEventArgs(WorldStateSymbol.ImportantLoot, wsValue));
      }
    }
  }

  public void AddToItemKnowledge(WorldStateSymbol symbol)
  {
    if (!HasItemLookup.ContainsKey(symbol))
    {
      HasItemLookup.Add(symbol, new InventoryItemStatus(0));
    }
    if (HasItemLookup[symbol].ItemCount == 0)
    {
      WorldStateVariableChanged.Invoke(this, new WsSymbolChangedEventArgs(symbol, true));
    }
    HasItemLookup[symbol].ItemCount++;
  }

  public void RemoveFromItemKnowledge(WorldStateSymbol symbol)
  {
    if (HasItemLookup.ContainsKey(symbol))
    {
      HasItemLookup[symbol].ItemCount--;

      if (HasItemLookup[symbol].ItemCount <= 0)
      {
        WorldStateVariableChanged.Invoke(this, new WsSymbolChangedEventArgs(symbol, false));
      }
    }
  }

  public bool CheckItemKnowledge(WorldStateSymbol symbol)
  {
    if (HasItemLookup.ContainsKey(symbol))
    {
      if (HasItemLookup[symbol].ItemCount > 0)
      {
        return true;
      }
    }
    return false;
  }

  public int CheckItemKnowledgeCount(WorldStateSymbol symbol)
  {
    if (HasItemLookup.ContainsKey(symbol))
    {
      return HasItemLookup[symbol].ItemCount;
    }
    return 0;
  }

  bool roomExplored;
  public bool RoomExplored
  {
    get { return roomExplored; }
    set
    {
      if (roomExplored != value)
      {
        roomExplored = value;
        WorldStateVariableChanged.Invoke(this, new WsSymbolChangedEventArgs(WorldStateSymbol.RoomExplored, roomExplored));
        WorldStateVariableChanged.Invoke(this, new WsSymbolChangedEventArgs(WorldStateSymbol.RoomUnexplored, !roomExplored));
      }
    }
  }


  public int Health { get { return GetComponent<Player>().Health; } }

  public int Coins { get { return GetComponent<Player>().Coins; } }

  /// <summary>
  /// Updated every second by the InvokeRepeating function in Start().
  /// </summary>
  public void UpdateTargets()
  {
    targetingService.Refresh();
    RoomExplored = Dungeon.Singleton.CurrentRoom.IsExplored;
  }
}

public class WsSymbolChangedEventArgs : EventArgs
{
  public WorldStateSymbol Symbol { get; private set; }
  public bool Value { get; private set; }

  public WsSymbolChangedEventArgs(WorldStateSymbol symbol, bool value)
  {
    Symbol = symbol;
    Value = value;
  }
}
