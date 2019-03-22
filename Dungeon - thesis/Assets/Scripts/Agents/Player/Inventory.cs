using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Inventory : MonoBehaviour
{
  public static event Action<int, int, Item> OnItemAdded = delegate { };
  public static event Action<int, int> OnItemRemoved = delegate { };

  //TODO Göra så att inventoryn uppdaterar blackboarden istället för player.cs
  BlackBoard blackBoard;
  int inventorySize = 5;
  List<Stack<Item>> items;

  private void Start()
  {
    items = new List<Stack<Item>>(inventorySize);
    for (int i = 0; i < inventorySize; i++)
    {
      items.Add(new Stack<Item>());
    }
    blackBoard = GetComponent<BlackBoard>();
  }

  public void AddItem(Item item)
  {
    foreach (Stack<Item> itemStack in items)
    {
      if (itemStack.Count == 0 || itemStack.Peek().GetType() == item.GetType())
      {
        itemStack.Push(item);
        OnItemAdded(items.IndexOf(itemStack), itemStack.Count, item);
        break;
      }
    }
  }

  public void RemoveItem(Item item)
  {
    foreach (Stack<Item> itemStack in items)
    {
      if (itemStack.Peek().GetType() == item.GetType())
      {
        itemStack.Pop();
        OnItemRemoved(items.IndexOf(itemStack), itemStack.Count);
        break;
      }
    }
  }

  public Item TryGetItem(Type itemtype)
  {
    foreach (Stack<Item> itemStack in items)
    {
      if (itemStack.Count > 0)
        if (itemStack.Peek().GetType() == itemtype)
        {
          OnItemRemoved(items.IndexOf(itemStack), itemStack.Count);
          return itemStack.Peek();
        }
    }
    return null;
  }

}
