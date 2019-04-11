using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;

public enum Personality { BRAVERY, BLOODLUST, GREED, EXPLORATION, PROGRESSION, }

public abstract class Persona : MonoBehaviour
{
  protected float finalOpinion = 0f;
  public Dictionary<Personality, float> personalityModifer = new Dictionary<Personality, float>
    {
      { Personality.BRAVERY, 0f },
      { Personality.BLOODLUST, 0f },
      { Personality.GREED, 0f },
      { Personality.EXPLORATION, 0f },
      { Personality.PROGRESSION, 0f }
    };
  public float enemyDistanceRange; // The distance of even being interested in an enemy

}
