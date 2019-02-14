using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using System;

[Serializable]
public class Tile
{
  public Tile(){}

  public Tile(Vector2 position, bool immutable)
  {
    Position = position;
    Immutable = immutable;
  }

  [XmlElement]
  public Vector2 Position { get; private set; }

  [XmlElement("m_immutable")]
  public bool Immutable { get; private set; }

  [XmlElement]
  public TileType Type { get; set; }
}
