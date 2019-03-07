using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour {
    private float scanRotation = 10f;
    private int sightRange = 4;
    LayerMask visionMask;

    public void Start() {
        visionMask = LayerMask.GetMask("Vision");
    }

    /// <summary>
    /// Scan the area around the agent for memorable tiles.
    /// </summary>
    public void Scan() {
        var dungeon = GameObject.FindWithTag("Dungeon").GetComponent<Dungeon>();

        Vector2 rayOrigin = transform.position;
        Vector2 rayDirection = Vector3.up;     

        for (int i = 0; i < 360 / scanRotation; i++) {
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, sightRange, visionMask);
            if (hit) {
                if (hit.collider.gameObject) {
                    var offsetHitLocation = hit.point + (rayDirection / 2);
                    //if (hit.collider.gameObject.name == "CollidingTileLayer") {
                    //    TileModel tile = dungeon.CurrentRoom.Tiles2D[(int)offsetHitLocation.x, (int)offsetHitLocation.y];
                    //    Debug.DrawLine(rayOrigin, offsetHitLocation, Color.red);
                    //}
                    if (hit.collider.gameObject.name != "CollidingTileLayer") {
                        Debug.DrawLine(rayOrigin, offsetHitLocation, Color.green);
                        GetComponent<BlackBoard>().AddPOI(hit.collider.gameObject, dungeon.CurrentRoom.Tiles2D[(int)offsetHitLocation.x, (int)offsetHitLocation.y].Type);
                    }
                }
            }
            rayDirection = Rotate(rayDirection);
        }
    }

    /// <summary>
    /// Simply rotates a Vector2 around its origin scanInterval degrees
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    private Vector2 Rotate(Vector2 v) {
        float sin = Mathf.Sin(scanRotation * Mathf.Deg2Rad);
        float cos = Mathf.Cos(scanRotation * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}
