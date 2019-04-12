using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Agent : MonoBehaviour
{
  public static event Action<Agent> OnIsEnabled = delegate { };
  public static event Action<Agent> OnIsDisabled = delegate { };

  public static readonly int maxHealth = 100;
  public int Health = maxHealth;
  protected float attackSpeed = 1.0f;

  [SerializeField]
  protected int damagePower = 25;

  public virtual event Action<float> OnHealthChanged = delegate { };

  public int Damage { get { return damagePower; } }

  public int MaxHealth { get { return maxHealth; } }

  public virtual void TakeDamage(GameObject attacker, int amount)
  {
    ModifyHealth(-amount);

    if (Health <= 0)
    {
      HandleDeath(attacker);
    }
  }

  protected virtual void HandleDeath(GameObject attacker)
  {
    OnIsDisabled(this);
    Destroy(gameObject);
  }

  private void OnDisable()
  {
    OnIsDisabled(this);
  }

  private void OnEnable()
  {
    OnIsEnabled(this);
  }

  public virtual void ModifyHealth(int amount)
  {
    Health += amount;
    NotifyHealthChange();

  }

  private void NotifyHealthChange()
  {
    float healthPct = (float)Health / (float)maxHealth;
    OnHealthChanged(healthPct);
  }



}
