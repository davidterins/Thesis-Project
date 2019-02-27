using System;
using System.IO;
using UnityEngine;


public class DungeonImporter : MonoBehaviour
{
  [SerializeField]
  readonly string mapFileName = "DungeonTest.map";

  public DungeonModel Dungeon { get { return ImportDungeon(); } }


  /// <summary>
  /// Returns a Dungeon created from speciefied map/xml file
  /// </summary>
  /// <returns>The dungeon.</returns>
  /// <param name="filePath">File path to map/xml file.</param>
  private DungeonModel ImportDungeon()
  {
    string fullMapFilePath = Path.Combine(Application.dataPath, "Maps/"+ mapFileName);
  

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



