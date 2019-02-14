using UnityEngine;
using System;
using System.Xml.Serialization;

[Serializable]
  public class SimpleXMLclass
  {
    public SimpleXMLclass()
    {
    }

  [XmlElement]
    public int Test { get; set; }
  }