using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Personality { BRAVERY, BLOODLUST, GREED, EXPLORATION, PROGRESSION, }

public abstract class Persona : MonoBehaviour {
    protected BlackBoard blackBoard;
    public Dictionary<Personality, float> personalityModifer;
    public float enemyDistanceRange; // The distance of even being interested in an enemy

    public virtual void Awake() {
        try {
            blackBoard = GetComponent<BlackBoard>();
        }
        catch {
            Debug.LogError("No BlackBoard attached!");
        }
        personalityModifer = new Dictionary<Personality, float>();
        personalityModifer.Add(Personality.BRAVERY, 0f);
        personalityModifer.Add(Personality.BLOODLUST, 0f);
        personalityModifer.Add(Personality.GREED, 0f);
        personalityModifer.Add(Personality.EXPLORATION, 0f);
        personalityModifer.Add(Personality.PROGRESSION, 0f);
    }

    public abstract float AttackEnemyFactor(GameObject closestEnemy);
    public abstract float LootTreasureFactor();
}
