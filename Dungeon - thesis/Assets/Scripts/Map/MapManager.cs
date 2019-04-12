using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class MapManager : MonoBehaviour
{
  [SerializeField]
  Settings.Persona FallBackPersona = Settings.Persona.Rusher;

  [SerializeField]
  string RusherDefaultMap = "RusherBase.map";

  [SerializeField]
  string TreasureHunterDefaultMap = "TreasureHunterBase.map";

  [SerializeField]
  string MonsterSlayerDefaultMap = "MonsterSlayerBase.map";

  public DungeonModel Dungeon { get { return BuildDungeonFromFile(); } }

  private string SelectMapFile()
  {
    Settings.Persona persona;

    if (Settings.GameStartedFromMenu)
    {
      persona = Settings.FetchPersona();
    }
    else
    {
      persona = FallBackPersona;
    }
    switch (persona)
    {
      case Settings.Persona.Rusher:
        return RusherDefaultMap;

      case Settings.Persona.TreasureHunter:
        return TreasureHunterDefaultMap;

      case Settings.Persona.MonsterSlayer:
        return MonsterSlayerDefaultMap;
    }
    return RusherDefaultMap;
  }

  /// <summary>
  /// Returns a Dungeon created from speciefied map/xml file
  /// </summary>
  /// <returns>The dungeon.</returns>
  private DungeonModel BuildDungeonFromFile()
  {
    //string fullMapFilePath = Path.Combine(Application.dataPath, "Maps/"+ mapFileName);
    string fullMapFilePath;
    if (Settings.GameStartedFromMenu)
    {
      fullMapFilePath = Settings.SelectedMapFromMenu;// FindObjectOfType<ImportInfo>().DungeonFilePath;
    }
    else
    {
      var mapName = SelectMapFile();
      fullMapFilePath = Path.Combine(Application.dataPath, "Maps/" + mapName);
    }

    FileInfo fileInfo = new FileInfo(fullMapFilePath);

    if (fileInfo.Exists)
    {
      DungeonModel dungeon = Serialization.DeserializeXMLFileToObject<DungeonModel>(fullMapFilePath);
      Debug.Log("Successfully created a dungeon from: " + fileInfo.Name);
      return dungeon;
    }
    else
    {
      string message = "Could not find map/xml file at: " + fullMapFilePath;
      Debug.LogWarning(message);
      throw new Exception(message);
    }
  }
}
