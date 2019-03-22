using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Agent : MonoBehaviour
{
  public static event Action<Agent> OnIsEnabled = delegate { };
  public static event Action<Agent> OnIsDisabled = delegate { };

  public static int maxHealth = 100;
  public int Health = maxHealth;
  protected float attackSpeed = 1.0f;

  [SerializeField]
  protected int damagePower = 25;

  public event Action<float> OnHealthChanged = delegate { };

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
    float healthPct = (float)Health / (float)maxHealth;
    OnHealthChanged(healthPct);

    if (Health < 0)
      Health = 0;

    Debug.Log(gameObject.name + " Health: " + Health);
  }

  protected virtual void HandleDeath(GameObject attacker)
  {
    Destroy(gameObject);
  }

  private void OnEnable()
  {
    OnIsEnabled(this);
  }


}
