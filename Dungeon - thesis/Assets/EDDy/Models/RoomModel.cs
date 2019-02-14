using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class RoomModel
{
  public RoomModel(){}

  [XmlElement]
  public Tile[] Tiles { get; set; }

  [XmlElement]
  public double EntranceSafety { get; set; }
}
