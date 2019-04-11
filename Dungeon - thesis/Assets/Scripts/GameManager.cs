using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  private static GameManager Instance;
  public static GameManager Singleton { get { return Instance; } }

  void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
  }
  public void StartGame()
  {

  }

  public void Complete()
  {

  }

  public void GameOver()
  {

  }

  public void Restart()
  {

  }

}
