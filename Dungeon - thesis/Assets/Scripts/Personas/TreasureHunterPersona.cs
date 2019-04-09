using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureHunterPersona : Persona {

    public override void Awake() {
        base.Awake();
        enemyDistanceRange = GetComponent<Vision>().GetSightRange() - 1f;
        personalityModifer[Personality.BRAVERY] = 0.2f;
        personalityModifer[Personality.BLOODLUST] = 0.15f;
        personalityModifer[Personality.GREED] = 0.9f;
        personalityModifer[Personality.EXPLORATION] = 0.6f;
        personalityModifer[Personality.PROGRESSION] = 0.1f;
    }
}
