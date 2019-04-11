using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPersona : Persona {

    private void Awake() {
        enemyDistanceRange = GetComponent<Vision>().GetSightRange() - 1f;
        personalityModifer[Personality.BRAVERY] = 0.5f;
        personalityModifer[Personality.BLOODLUST] = 0.4f;
        personalityModifer[Personality.GREED] = 0.4f;
        personalityModifer[Personality.EXPLORATION] = 0.3f;
        personalityModifer[Personality.PROGRESSION] = 0.2f;       
    }
}
