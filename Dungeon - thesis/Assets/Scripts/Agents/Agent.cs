using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
  public static int maxHealth = 100;
  public int Health = maxHealth;
  protected float attackSpeed = 1.0f;

  [SerializeField]
  protected int damagePower = 25;

  public int Damage { get { return damagePower; } }

  public int MaxHealth { get { return maxHealth; } }

  public virtual void HandlePickup(Item item) { }

  public virtual void TakeDamage(GameObject attacker, int amount)
  {
    if (Health - amount < 0)
    {
      HandleDeath(attacker);
    }
    Health -= amount;

    Debug.Log(gameObject.name + " Health: " + Health);
  }

  protected virtual void HandleDeath(GameObject attacker)
  {
    Destroy(gameObject);
  }
}
