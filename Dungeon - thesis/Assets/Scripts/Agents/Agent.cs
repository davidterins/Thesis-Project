using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {
    public static int maxHealth = 100;
    public int Health = maxHealth;
    protected float attackSpeed = 1.0f;

    public int MaxHealth { get { return maxHealth; } }

    public virtual void HandlePickup(Item item) {

    }

    public void TakeDamage(GameObject attacker, int amount) {
        if (Health - amount < 0) {
            HandleDeath(attacker);
        }
    }

    protected virtual void HandleDeath(GameObject attacker) {
        Destroy(gameObject);
    }
}
