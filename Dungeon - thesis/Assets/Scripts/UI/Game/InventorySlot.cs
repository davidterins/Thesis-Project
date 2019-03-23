using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
  [SerializeField]
  Text StackText;

  [SerializeField]
  Image ItemImage = null;

  [SerializeField]
  Sprite DefaultSlot = null;

  public void SetItem(Item item, int itemCount)
  {
    ItemImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
    StackText.text = itemCount.ToString();
  }

  public void RemoveItem(int itemCount)
  {
    if (itemCount == 0)
    {
      StackText.text = string.Empty;
      ItemImage.sprite = DefaultSlot;
    }
    else
      StackText.text = itemCount.ToString();
  }

}
