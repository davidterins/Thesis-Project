using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{

  public int Health = 100;


  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public virtual void HandlePickup(Item item)
  {

  }

  public void TakeDamage(int amount)
  {
    if(Health - amount < 0)
    {
      HandleDeath();
    }
  }

  protected virtual void HandleDeath()
  {
    Destroy(gameObject);
  }
}
