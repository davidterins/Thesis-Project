using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
  InventorySlot[] Slots;

  void Awake()
  {
    Slots = GetComponentsInChildren<InventorySlot>();

    Inventory.OnItemAdded += Inventory_OnItemAdded;
    Inventory.OnItemRemoved += Inventory_OnItemRemoved;
  }

  void Inventory_OnItemAdded(int slotIndex, int itemCount, Item item)
  {
    Slots[slotIndex].SetItem(item, itemCount);
   
  }

  void Inventory_OnItemRemoved(int slotIndex, int itemCount)
  {
    Slots[slotIndex].RemoveItem(itemCount);
  }

  private void OnDestroy()
  {
    Inventory.OnItemAdded -= Inventory_OnItemAdded;
    Inventory.OnItemRemoved -= Inventory_OnItemRemoved;
  }
}
