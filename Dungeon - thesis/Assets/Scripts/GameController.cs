using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  private static GameController Instance;
  public static GameController Singleton { get { return Instance; } }

  void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
   
  }

  private void Start()
  {
    StartGame();
  }


  public void Initialize()
  {

  }

  public void StartGame()
  {
    //TODO do start game with correct map parameter
    Dungeon.Singleton.CreateDungeon(GetComponent<MapManager>().Dungeon);
  }

  public void Complete()
  {
    //TODO Visa output
  }

  public void GameOver()
  {
    //TODO Visa output
  }

  public void Restart()
  {

  }
}
