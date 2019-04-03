using UnityEngine;


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
    if (blackBoard.Memory.ContainsKey(typeof(Enemy)))
    {
      Vector2 ownPos = owner.transform.position;
      float closestDistance = float.MaxValue;

      foreach (GameObject enemy in blackBoard.Memory[typeof(Enemy)])
      {
        if (enemy != null)
        {//TODO Kolla först efter enemies i samma rum.s
          var enemyScript = enemy.GetComponent<Enemy>();
          if (enemyScript.RoomID == Dungeon.Singleton.CurrentRoom.RoomID)
          {
            float distance = Vector2.Distance(ownPos, enemy.transform.position);
            if (distance < closestDistance)
            {
              if (closestTarget)
              {
                if (enemy.GetComponent<Agent>().Health < closestTarget.GetComponent<Agent>().Health)
                {
                  closestTarget = enemy;
                  closestDistance = distance;
                }
              }
              else
              {
                closestTarget = enemy;
                closestDistance = distance;
              }
            }
          }

        }
      }
    }
    blackBoard.TargetEnemyObject = closestTarget;
  }


  public void TrySetTreasureChest()
  {
    GameObject closestTarget = null;
    if (blackBoard.Memory.ContainsKey(typeof(TreasureChest)))
    {
      Vector2 ownPos = owner.transform.position;
      float closestDistance = float.MaxValue;

      foreach (GameObject treasure in blackBoard.Memory[typeof(TreasureChest)])
      {
        if (treasure)
        {
          var treasureScript = treasure.GetComponent<TreasureChest>();
          if (treasureScript.RoomID == Dungeon.Singleton.CurrentRoom.RoomID)
          {
            if (treasureScript.IsClosed)
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
    }
    blackBoard.TargetTreasureChest = closestTarget;
  }

}
