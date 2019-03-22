using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  [SerializeField]
  Image ForeGroundImage = null;

  [SerializeField]
  float UpdateForSeconds = 0.2f;

  [SerializeField]
  float offsetPosition = 1;

  private Agent agent;

  public void SetHealth(Agent agent)
  {
    this.agent = agent;
    agent.OnHealthChanged += Handle_HealthChanged;
  }

  void Handle_HealthChanged(float healthPct)
  {
    StartCoroutine(UpdateHealth(healthPct));
  }

  private IEnumerator UpdateHealth(float healthPct)
  {
    float preChangePct = ForeGroundImage.fillAmount;
    float elapsed = 0f;

    while (elapsed < UpdateForSeconds)
    {
      elapsed += Time.deltaTime;
      ForeGroundImage.fillAmount = Mathf.Lerp(preChangePct, healthPct, elapsed / UpdateForSeconds);
      yield return null;
    }
    ForeGroundImage.fillAmount = healthPct;
  }

  private void LateUpdate()
  {
    transform.position = Camera.main.WorldToScreenPoint(agent.transform.position + Vector3.up * offsetPosition);
  }

  private void OnDestroy()
  {
    agent.OnHealthChanged -= Handle_HealthChanged;
  }

}
