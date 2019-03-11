using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    public int Health = 100;
    protected float attackSpeed = 1.0f;

    public virtual void HandlePickup(Item item) {

    }

    public void TakeDamage(int amount) {
        if (Health - amount < 0) {
            HandleDeath();
        }
    }

    protected virtual void HandleDeath() {
        Destroy(gameObject);
    }
}
