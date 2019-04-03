using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InfoBox
{
  public static string targetTile = "";
  public static string playerTile = "";
  public static int pathLength = 0;
  public static int stepsLeft = 0;
  private static string memory = "";
  public static int hp = 100;
  public static int coins;

  /// <summary>
  /// Add to memory and trim the "(Clone)"-part
  /// </summary>
  /// <param name="container">Item to add</param>
  public static void UpdateMemory(Dictionary<Type, List<GameObject>> container)
  {
    memory = "";
    foreach (var items in container.Values)
    {
      foreach (GameObject go in items)
      {
        if(go)
        {
          IMemorizable memorizableObject = go.GetComponent<IMemorizable>();
          memory += "\n" + go.name.Substring(0, go.name.Length - 7) + " Room: " + memorizableObject.RoomID;
        }
      }
    }
  }

  public static string GetMemory()
  {
    return memory;
  }
}
