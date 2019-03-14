using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy_Goal : Goal_Goap {
    public KillEnemy_Goal(GameObject owner, Planner_Goap planner) : base(owner, planner ) {
        GoalWorldstates.Add(WorldStateSymbol.EnemyDead, true);
    }

    // Calculate relevancy to attack something.
    public override float CalculateRelevancy(BlackBoard blackBoard) {
    //relevancy = blackBoard.GetAttackEnemyRelevancy();
    // How close is the closest enemy? Closer distance -> increased starting factor
    // Ex: Bloodlust = 0.5 & Bravery = 0.5 and distance 5 tiles gives us: 0.5 + 0.5 - 0.5 = 0.5.
    relevancy = persona.personalityModifer[Personality.BLOODLUST] +
                   persona.personalityModifer[Personality.BRAVERY] -
                   (Vector2.Distance(blackBoard.EnemyObject.transform.position, owner.transform.position) / 10);

    // Calculate how many enemies are around apart from the closest one, too many will scare the agent off!
    // ...unless the agent is very brave, in which case it's even more inclined to attack!
    foreach (GameObject enemy in blackBoard.Memory[TileType.ENEMY])
    {
      if (enemy != null)
      {
        if (enemy != blackBoard.EnemyObject)
        {
          float distance = Vector2.Distance(owner.transform.position, enemy.transform.position);
          if (distance < persona.enemyDistanceRange)
            relevancy -= (1f - (distance / 10) - persona.personalityModifer[Personality.BRAVERY]) / 10;
        }
      }
    }
    // Very close enemy and ~0.5 in bravery/bloodlust ~-> 0.9
    // Very far enemy and ~0.5 in bravery/bloodlust ~-> 0.5
    return relevancy;
   // return relevancy;
        //return base.CalculateRelevancy(blackBoard);
    }


}
