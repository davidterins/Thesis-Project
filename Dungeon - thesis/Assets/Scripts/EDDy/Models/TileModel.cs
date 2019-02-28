using UnityEngine;
using System;
using System.Xml.Serialization;

[Serializable]
[XmlRoot(ElementName ="Tile")]
public class TileModel
{
  public TileModel(){}

  public TileModel(Vector2 Position, bool Immutable)
  {
    this.Position = Position;
    this.Immutable = Immutable;
  }

  public Vector2 Position { get; set; }

  public bool Immutable { get; set; }

  public TileType Type { get; set; }
}
