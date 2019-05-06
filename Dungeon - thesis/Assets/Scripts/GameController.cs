using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
  public static event Action OnShowOutput = delegate { };
  public static event Action<int> OnIterationChanged = delegate { };

  private static GameController Instance;
  public static GameController Singleton { get { return Instance; } }

  public static int GameCurrentIteration = 1;


  void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;

    Player.OnPlayerDeath += GameOver;
    Portal.OnPortalEnter += Complete;
  }

  private void OnDestroy()
  {
    Player.OnPlayerDeath -= GameOver;
    Portal.OnPortalEnter -= Complete;
  }

  private void Start()
  {
    StartGame();
  }

  public void StartGame()
  {
    Dungeon.Singleton.CreateDungeon(GetComponent<MapManager>().Dungeon);
  }

  public void Complete()
  {
    StopOrIterate();
  }

  public void GameOver()
  {
    StopOrIterate();
  }

  private void StopOrIterate()
  {
    if (GameCurrentIteration <= 1)
    {
      OnShowOutput.Invoke();
    }
    else
    {
      GameCurrentIteration--;
      Restart();
    }
  }

  public void Restart()
  {
    //TotalGameIterations = Settings.Iterations;
    //Output.Cards = new List<RoomCardModel>();

    SceneManager.LoadScene("DungeonScene");
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.I))
    {
      Restart();
    }
  }
}
