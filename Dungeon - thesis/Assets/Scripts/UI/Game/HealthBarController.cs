using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
  [SerializeField]

  HealthBar healthBarPrefab;

  Dictionary<Agent, HealthBar> healthBars;

  private void Awake()
  {
    healthBars = new Dictionary<Agent, HealthBar>();
    Agent.OnIsEnabled += Agent_OnIsEnabled;
    Agent.OnIsDisabled += Agent_OnIsDisabled;
  }

  void Agent_OnIsDisabled(Agent obj)
  {
    if (healthBars.ContainsKey(obj))
    {
      Destroy(healthBars[obj].gameObject);
      healthBars.Remove(obj);
    }
  }


  void Agent_OnIsEnabled(Agent obj)
  {
    if (!healthBars.ContainsKey(obj))
    {
      var healthBar = Instantiate(healthBarPrefab, transform);
      healthBars.Add(obj, healthBar);
      healthBar.SetHealth(obj);
    }
  }

  private void OnDestroy()
  {
    Agent.OnIsEnabled -= Agent_OnIsEnabled;
    Agent.OnIsDisabled -= Agent_OnIsDisabled;
  }



}
