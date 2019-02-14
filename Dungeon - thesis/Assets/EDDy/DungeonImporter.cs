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

    string rootPath = Application.dataPath;
    Debug.Log(rootPath);


    //var mapPath = Path.Combine(rootPath, "Maps/simpleTest.map");
    var mapPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    mapPath = Path.Combine(mapPath, "EddyMaps/lp.map");
    Debug.Log(mapPath);

    try
    {

      bool exists = File.Exists(mapPath);
      if (exists)
      {
        SimpleXMLclass test = Serialization.DeserializeXMLFileToObject<SimpleXMLclass>(mapPath);
        // Serialization.XMLDeserialize<SimpleXMLclass>(mapPath);
        var lol = test.Test;
        //DungeonModel dungeon = XMLDeserialize<DungeonModel>(mapPath);
      }
      else
        Debug.LogWarning("Could not fine file at: " + mapPath);
    }
    catch (Exception ex)
    {
      Debug.LogError(ex.StackTrace);
    }
  }


 


}



