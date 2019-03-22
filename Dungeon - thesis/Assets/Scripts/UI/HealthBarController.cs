using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
  [SerializeField]
  HealthBar healthBarPrefab;
  Dictionary<Agent, HealthBar> healthBars;

  private void Start()
  {
    //healthBars = new Dictionary<Agent, HealthBar>();
    //Agent.OnIsEnabled += Agent_OnIsEnabled;
    //Agent.OnIsDisabled += Agent_OnIsDisabled;
  }

  void Agent_OnIsDisabled(Agent obj)
  {
    //if (!healthBars.ContainsKey(obj))
    //{
    //  obj.OnHealthChanged -= Obj_OnHealthChanged;
    //}
  }


  void Agent_OnIsEnabled(Agent obj)
  {
    //if (!healthBars.ContainsKey(obj))
    //{
    //  obj.OnHealthChanged += Obj_OnHealthChanged;
    //  var healthBar = Instantiate(healthBarPrefab, transform);
    //  healthBars.Add(obj, healthBar);
    //  healthBar.
    //}

  }

  void Obj_OnHealthChanged(float obj)
  {

  }

}
