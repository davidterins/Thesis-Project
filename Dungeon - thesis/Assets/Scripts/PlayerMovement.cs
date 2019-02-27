using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {
    // Normal Movements Variables
    private float walkSpeed;
    private float curSpeed;
    private float maxSpeed;
    private Graph pathGraph;
    private bool hasTarget, interruptPath;
    private List<Node> path;

    //private CharacterStat plStat;

    void Start(Graph pathGraph) {
        //plStat = GetComponent<CharacterStat>();
        this.pathGraph = pathGraph;
        hasTarget = false;
        interruptPath = false;
        walkSpeed = 1; //(float)(plStat.Speed + (plStat.Agility / 5));
        maxSpeed = 4;// walkSpeed + (walkSpeed / 2);

    }

    void FixedUpdate() {
        curSpeed = walkSpeed;
        maxSpeed = curSpeed;

        // Acquire path if agent has no target and isn't instructed to change path
        if (!hasTarget || interruptPath) {
            path = AcquirePath();
            hasTarget = true;
            interruptPath = false;
        }

        // Move senteces
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * curSpeed, 0.8f),
        Mathf.Lerp(0, Input.GetAxis("Vertical") * curSpeed, 0.8f));
    }

    List<Node> AcquirePath() {
        return pathGraph.FindPath(this.transform.position, null);
    }
}

