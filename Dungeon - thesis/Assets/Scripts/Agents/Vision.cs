using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
  private float scanRotation = 10f;
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
          if(memorizableObject.OfInterest)
          {
            if(memorizableObject.MemorizableType == typeof(Portal))
            {
            
            }
            GetComponent<BlackBoard>().AddTypePOI(memorizableObject.MemorizableType, collision.gameObject);
            memorizableObject.OfInterest = false;
          }
         
        }
        //var cellPos = dungeon.WorldGrid.WorldToCell(collision.gameObject.transform.position);
        //GetComponent<BlackBoard>().AddPOI(dungeon.CurrentRoom.Tiles2D[cellPos.y, cellPos.x].Type, collision.gameObject);
        //Debug.DrawLine(transform.position, collision.gameObject.transform.position, Color.green);
      }
    }
    dungeon.CurrentRoom.RoomGraph.ExploreNodes(transform.position, sightRange);
  }

  public float GetSightRange()
  {
    return sightRange;
  }

  /// <summary>
  /// Simply rotates a Vector2 around its origin scanInterval degrees
  /// </summary>
  /// <param name="v"></param>
  /// <returns></returns>
  private Vector2 Rotate(Vector2 v)
  {
    float sin = Mathf.Sin(scanRotation * Mathf.Deg2Rad);
    float cos = Mathf.Cos(scanRotation * Mathf.Deg2Rad);

    float tx = v.x;
    float ty = v.y;
    v.x = (cos * tx) - (sin * ty);
    v.y = (sin * tx) + (cos * ty);
    return v;
  }
}
