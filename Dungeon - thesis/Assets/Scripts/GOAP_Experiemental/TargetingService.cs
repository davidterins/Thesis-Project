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

  //Called once per second in the BlackBoard
  public void Refresh()
  {
    TrySetEnemyTarget();
    TrySetTreasureChest();
  }

  public void TrySetEnemyTarget()
  { //Look through memory facts
    GameObject closestTarget = null;
    if (blackBoard.Memory[TileType.ENEMY].Count > 0)
    {
      Vector2 ownPos = owner.transform.position;
      float closestDistance = float.MaxValue;

      foreach (GameObject enemy in blackBoard.Memory[TileType.ENEMY])
      {
        if (enemy != null)
        {//TODO Kolla först efter enemies i samma rum.s
          var enemyScript = enemy.GetComponent<Enemy>();
          if (enemyScript.RoomID == Dungeon.Singleton.CurrentRoom.RoomID)
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
    }
    blackBoard.EnemyObject = closestTarget;
  }


  public void TrySetTreasureChest()
  {
    GameObject closestTarget = null;
    if (blackBoard.Memory[TileType.TREASURE].Count > 0)
    {
      Vector2 ownPos = owner.transform.position;
      float closestDistance = float.MaxValue;

      foreach (GameObject treasure in blackBoard.Memory[TileType.TREASURE])
      {
        if (treasure)
        {
          if (treasure.GetComponent<TreasureChest>().IsClosed)
          {
            float distance = Vector2.Distance(ownPos, treasure.transform.position);
            if (distance < closestDistance)
            {
              closestTarget = treasure;
              closestDistance = distance;
            }
          }
        }
      }
    }
    blackBoard.TreasureObject = closestTarget;
  }

}
