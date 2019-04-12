using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
  private int sightRange = 4;
  LayerMask visionMask;
  Collider2D[] inVision = new Collider2D[15];

  public void Start()
  {
    visionMask = LayerMask.GetMask("Vision");
  }

  /// <summary>
  /// Scan the area around the agent for memorable tiles.
  /// </summary>
  public void Scan()
  {
    var dungeon = Dungeon.Singleton;

    Physics2D.OverlapCircleNonAlloc(transform.position, sightRange, inVision, visionMask);
    foreach (var collision in inVision)
    {
      if (collision)
      {
        IMemorizable memorizableObject = collision.GetComponent<IMemorizable>();
        if (memorizableObject != null)
        {
          if (memorizableObject.OfInterest)
          {
            GetComponent<BlackBoard>().AddTypePOI(memorizableObject.MemorizableType, collision.gameObject);
            memorizableObject.OfInterest = false;
          }

        }
      }
    }
    dungeon.CurrentRoom.RoomGraph.ExploreNodes(transform.position, sightRange -1);
  }

  public float GetSightRange()
  {
    return sightRange;
  }
}
