using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPersona : Persona {

    public override void Awake() {
        base.Awake();
        enemyDistanceRange = GetComponent<Vision>().GetSightRange() - 1f;
        personalityModifer[Personality.BRAVERY] = 0.5f;
        personalityModifer[Personality.BLOODLUST] = 0.5f;
        personalityModifer[Personality.GREED] = 0.4f;
        personalityModifer[Personality.EXPLORATION] = 0.3f;
        personalityModifer[Personality.PROGRESSION] = 0.1f;       
    }

    // TODO: Add levels on enemies and take that into account
    // TODO: Add option to want to simply flee
    public override float AttackEnemyFactor(GameObject closestEnemy) { 
        // How close is the closest enemy? Closer distance -> increased starting factor
        // Ex: Bloodlust = 0.5 & Bravery = 0.5 and distance 5 tiles gives us: 0.5 + 0.5 - 0.5 = 0.5.
        float factor = personalityModifer[Personality.BLOODLUST] + 
                       personalityModifer[Personality.BRAVERY] - 
                       (Vector2.Distance(closestEnemy.transform.position, transform.position) / 10);

        // Calculate how many enemies are around apart from the closest one, too many will scare the agent off!
        // ...unless the agent is very brave, in which case it's even more inclined to attack!
        foreach (GameObject enemy in blackBoard.Memory[TileType.ENEMY]) {
            if (enemy != null) {
                if (enemy != closestEnemy) {
                    float distance = Vector2.Distance(transform.position, enemy.transform.position);
                    if (distance < enemyDistanceRange)
                        factor -= (1f - (distance / 10) - personalityModifer[Personality.BRAVERY]) / 10;
                }               
            }
        }
        // Very close enemy and ~0.5 in bravery/bloodlust ~-> 0.9
        // Very far enemy and ~0.5 in bravery/bloodlust ~-> 0.5
        return factor;
    }

    public override float LootTreasureFactor() {
        return 0.666f;  // Placeholder
    }
}
