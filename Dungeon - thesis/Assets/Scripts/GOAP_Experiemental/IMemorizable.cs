using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IMemorizable
{
  Type MemorizableType { get; }

  bool OfInterest { get; set; }


}
