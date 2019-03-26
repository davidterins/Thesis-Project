using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Inventory : MonoBehaviour
{
  /// <summary>
  /// UI events
  /// </summary>
  public static event Action<int, int, Item> OnItemAdded = delegate { };
  public static event Action<int, int> OnItemRemoved = delegate { };

  BlackBoard blackBoard;
  int inventorySize = 5;
  List<Stack<Item>> items;

  private void Start()
  {
    blackBoard = GetComponent<BlackBoard>();

    items = new List<Stack<Item>>(inventorySize);
    for (int i = 0; i < inventorySize; i++)
    {
      items.Add(new Stack<Item>());
    }
  }

  /// <summary>
  /// Adds the item.
  /// </summary>
  /// <param name="item">Item.</param>
  public void AddItem(Item item)
  {
    foreach (Stack<Item> itemStack in items)
    {
      if (itemStack.Count == 0 || itemStack.Peek().GetType() == item.GetType())
      {
        itemStack.Push(item);
        blackBoard.AddToItemKnowledge(item.ItemWSEffector);
        OnItemAdded(items.IndexOf(itemStack), itemStack.Count, item);
        item.OnItemUse += Handle_OnItemUse;
        break;
      }
    }
  }

  /// <summary>
  /// Tries to get an item from the inventory via the out variable.
  /// </summary>
  /// <param name="itemtype">Itemtype.</param>
  /// <param name="refItem">Reference item (Will be null if return is false).</param>
  public bool TryGetItem(Type itemtype, out Item refItem)
  {
    foreach (Stack<Item> itemStack in items)
    {
      if (itemStack.Count > 0)
        if (itemStack.Peek().GetType() == itemtype)
        {
          refItem = itemStack.Peek();
          return true;
        }
    }
    refItem = null;
    return false;
  }

  /// <summary>
  /// Triggered by <see cref="Item.OnItemUse"/>
  /// </summary>
  /// <param name="item">Item.</param>
  private void Handle_OnItemUse(Item item)
  {
    RemoveItem(item);
  }


  /// <summary>
  /// The item is removed when an item is used.
  /// </summary>
  /// <param name="item">Item.</param>
  private void RemoveItem(Item item)
  {
    foreach (Stack<Item> itemStack in items)
    {
      if(itemStack.Count > 0)
      {
        if (itemStack.Peek().GetType() == item.GetType())
        {
          itemStack.Pop();
          blackBoard.RemoveFromItemKnowledge(item.ItemWSEffector);
          OnItemRemoved(items.IndexOf(itemStack), itemStack.Count);
          item.OnItemUse -= Handle_OnItemUse;
          break;
        }
      }

    }
  }
}

public class InventoryItemStatus
{
  public int ItemCount { get; set; }
  public InventoryItemStatus(int itemCount)
  {
    ItemCount = itemCount;
  }
}