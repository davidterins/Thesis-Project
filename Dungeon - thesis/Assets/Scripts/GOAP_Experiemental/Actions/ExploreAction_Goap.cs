using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExploreAction_Goap : Action_Goap {
    protected float interactionRange = 0.001f;
    protected Vector2 target;
    protected Movement movement;

    protected ExploreAction_Goap(GameObject owner) : base(owner) { }

    public override void Enter() {
        base.Enter();
        if (!IsInRange()) {
            movement = owner.GetComponent<Movement>();
            // TODO: Call on graph.GetUnexploredPosition() and move to that location.
        }
    }

    public abstract bool IsInRange();

    public override void Execute() {
        base.Execute();
    }
}
