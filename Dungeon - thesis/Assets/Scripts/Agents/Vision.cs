﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour {
    private float scanRotation = 10f;
    private int sightRange = 4;
    LayerMask visionMask;
    Collider2D[] inVision = new Collider2D[15];

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

        Physics2D.OverlapCircleNonAlloc(transform.position, sightRange, inVision, visionMask);
        foreach (var collision in inVision) {
            if (collision) {
                var cellPos = dungeon.WorldGrid.WorldToCell(collision.gameObject.transform.position);
                GetComponent<BlackBoard>().AddPOI(dungeon.CurrentRoom.Tiles2D[cellPos.y, cellPos.x].Type, collision.gameObject);
                Debug.DrawLine(transform.position, collision.gameObject.transform.position, Color.green);
            }
        }
    }

    public float GetSightRange() {
        return sightRange;
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
