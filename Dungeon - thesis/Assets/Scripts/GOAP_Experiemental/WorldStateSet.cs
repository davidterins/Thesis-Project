using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System;

public class WorldStateSet : Dictionary<WorldState,bool>, ICloneable
{
  public object Clone()
  {
    WorldStateSet clone = new WorldStateSet();
    foreach (KeyValuePair<WorldState,bool> kvp in this)
    {
      clone.Add(kvp.Key, kvp.Value);
    }
    return clone;
  }
}
