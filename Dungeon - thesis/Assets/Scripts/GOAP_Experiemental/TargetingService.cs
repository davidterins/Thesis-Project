using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.Experimental.UIElements;

public class TargetingService
{
  BlackBoard blackBoard;
  GameObject owner;

  public TargetingService(GameObject owner)
  {
    this.owner = owner;
    blackBoard = owner.GetComponent<BlackBoard>();
  }

  //To be done once per second.
  public void Refresh()
  {

  }

  public GameObject TryGetEnemyTarget()
  { //Look through memory facts
    GameObject closestTarget = null;
    if (blackBoard.Memory[TileType.ENEMY].Count > 0)
    {
      Vector2 ownPos = owner.transform.position;
      float closestDistance = float.MaxValue;

      foreach (GameObject enemy in blackBoard.Memory[TileType.ENEMY])
      {
        if (enemy != null)
        {
          float distance = Vector2.Distance(ownPos, enemy.transform.position);
          if (distance < closestDistance)
          {
            closestTarget = enemy;
            closestDistance = distance;
          }
        }
      }
    }
      return closestTarget;
  }

  public void GetItemTarget()
  {

  }
}
