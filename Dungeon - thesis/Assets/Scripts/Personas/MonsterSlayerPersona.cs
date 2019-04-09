using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSlayerPersona : Persona {

    public override void Awake() {
        base.Awake();
        enemyDistanceRange = GetComponent<Vision>().GetSightRange() - 1f;
        personalityModifer[Personality.BRAVERY] = 0.9f;
        personalityModifer[Personality.BLOODLUST] = 0.9f;
        personalityModifer[Personality.GREED] = 0.35f;
        personalityModifer[Personality.EXPLORATION] = 0.45f;
        personalityModifer[Personality.PROGRESSION] = 0.2f;
    }
}
