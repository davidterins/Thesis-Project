using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Custom persona, created in the main menu.
/// </summary>
public class CustomPersona : Persona {

    public override void Awake() {
        base.Awake();
        enemyDistanceRange = GetComponent<Vision>().GetSightRange() - 1f;
        personalityModifer[Personality.BRAVERY] = 0.5f;
        personalityModifer[Personality.BLOODLUST] = 0.4f;
        personalityModifer[Personality.GREED] = 0.4f;
        personalityModifer[Personality.EXPLORATION] = 0.3f;
        personalityModifer[Personality.PROGRESSION] = 0.2f;
    }

    /// <summary>
    /// Sets a personality trait to the passed value.
    /// </summary>
    /// <param name="personalityTrait">The personality trait to set.</param>
    /// <param name="value">The value of the chosen personality trait.</param>
    public void SetValues(float[] modifiers) {
        //personalityModifer[personalityTrait] = value;
    }
}
