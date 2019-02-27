using UnityEngine;
using System;

[Serializable]
public class Tile
{
  public Tile(){}

  public Tile(Vector2 Position, bool Immutable)
  {
    this.Position = Position;
    this.Immutable = Immutable;
  }

  public Vector2 Position { get; set; }

  public bool Immutable { get; set; }

  public TileType Type { get; set; }
}
