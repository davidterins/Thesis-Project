using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

  private void OnCollisionEnter2D(Collision2D collision)
  {
    DoorEnter();
  }

  void DoorEnter()
  {
    Debug.Log("Collided with Exit Door");
  }
}
