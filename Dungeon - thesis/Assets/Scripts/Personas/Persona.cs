using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Personality { BRAVERY, BLOODLUST, GREED, EXPLORATION, PROGRESSION, }

public abstract class Persona : MonoBehaviour
{
  protected BlackBoard blackBoard;
  public Dictionary<Personality, float> personalityModifer = new Dictionary<Personality, float>
    {
      { Personality.BRAVERY, 0f },
      { Personality.BLOODLUST, 0f },
      { Personality.GREED, 0f },
      { Personality.EXPLORATION, 0f },
      { Personality.PROGRESSION, 0f }
    };
  public float enemyDistanceRange; // The distance of even being interested in an enemy

  public virtual void Awake()
  {
    try
    {
      blackBoard = GetComponent<BlackBoard>();
    }
    catch
    {
      Debug.LogError("No BlackBoard attached!");
    }
    //personalityModifer = new Dictionary<Personality, float>
    //{
    //  { Personality.BRAVERY, 0f },
    //  { Personality.BLOODLUST, 0f },
    //  { Personality.GREED, 0f },
    //  { Personality.EXPLORATION, 0f },
    //  { Personality.PROGRESSION, 0f }
    //};
  }
}
