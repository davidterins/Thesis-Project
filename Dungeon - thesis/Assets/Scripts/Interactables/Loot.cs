using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
  [SerializeField]
  List<GameObject> items = new List<GameObject>();
  public List<GameObject> Items { get { return items; } }

  [SerializeField]
  Coin coin = null;


  // TODO create a lootManager instead of passing in GameObject
  public void Drop(GameObject looter, Vector2 dropPosition)
  {
    Instantiate(coin, dropPosition, Quaternion.identity);

    var tempLootList = new List<GameObject>(Items.Count);
    foreach (var item in Items)
    {
      float dropRateValue = Random.Range(0.00f, 1.00f);
      Item itemScript = item.GetComponent<Item>();

    
      // TODO Göra så att agenten har någon "Itemimportance function som säger
      // hur viktigt ett visst item är att plocka upp.
      if (dropRateValue <= itemScript.GetDropRate())
      {
        var lootObj = Instantiate(item, dropPosition, Quaternion.identity);
        tempLootList.Add(lootObj);

        if(itemScript.GetImportance() == 1f)
        {//Keys and potions
          looter.GetComponent<BlackBoard>().ImportantItemDrop = lootObj;
          //tempLootList.Add(lootObj);
        }


      }


      //if (item.GetComponent<Key>()|| item.GetComponent<Potion>())
      //{
      //  var lootObj = Instantiate(item, dropPosition, Quaternion.identity);
      
      //}

    }

    if (tempLootList.Count > 0)
      looter.GetComponent<BlackBoard>().TargetLoot = tempLootList;
  }
}
