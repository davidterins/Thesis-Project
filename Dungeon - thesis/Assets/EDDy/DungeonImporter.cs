using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using System.Text;
using System.Xml;


public class DungeonImporter : MonoBehaviour
{

  void Start()
  {
    //Hard coded path to DungeonTest.
    string mapPath = Application.dataPath;
    mapPath = Path.Combine(mapPath, "Maps/DungeonTest.map");

    DungeonModel DaDungeon = ImportDungeon(mapPath);
  }

  /// <summary>
  /// Returns a Dungeon created from speciefied map/xml file
  /// </summary>
  /// <returns>The dungeon.</returns>
  /// <param name="filePath">File path to map/xml file.</param>
  public DungeonModel ImportDungeon(string filePath)
  {
    FileInfo fileInfo = new FileInfo(filePath);

    if (fileInfo.Exists)
    {
      DungeonModel dungeon = Serialization.DeserializeXMLFileToObject<DungeonModel>(filePath);
      Debug.Log("Successfully created a dungeon from: " + fileInfo.Name);
      return dungeon; 
    }
    else
    {
      string message = "Could not find map/xml file at: " + filePath;
      Debug.LogWarning(message);
      throw new Exception(message);
    }
  }





}



