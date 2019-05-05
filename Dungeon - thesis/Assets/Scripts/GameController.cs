using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
  public static event Action OnShowOutput = delegate { };

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

    Player.OnPlayerDeath += GameOver;
    Portal.OnPortalEnter += Complete;
  }

  private void Start()
  {
    StartGame();
  }

  public void StartGame()
  {
    //TODO do start game with correct map parameter
    Dungeon.Singleton.CreateDungeon(GetComponent<MapManager>().Dungeon);
  }

  public void Complete()
  {
    // TODO Kommentera in för att att iterera en dungeon flera gånger
    //if (Output.currentIteration > 0)
    //{
    //  Output.DecreaseTotalRuns();
    //  Restart();
    //}
    //{
    //  OnShowOutput.Invoke();
    //}

    OnShowOutput.Invoke();
  }

  public void GameOver()
  {
    // TODO Kommentera in för att att iterera en dungeon flera gånger
    //if (Output.currentIteration > 0)
    //{
    //  Output.DecreaseTotalRuns();
    //  Restart();
    //}
    //{
    //  OnShowOutput.Invoke();
    //}

    OnShowOutput.Invoke();
  }

  public void Restart()
  {
    Output.Cards = new List<RoomCardModel>();
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
