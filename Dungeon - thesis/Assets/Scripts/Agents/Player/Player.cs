using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent {
    public int Coins = 0;

    public override void HandlePickup(Item item) {
        if (item.GetType() == typeof(Coin)) {
            Coins += ((Coin)item).value;
            //var cellPos = GameObject.FindWithTag("Dungeon").GetComponent<Dungeon>().WorldGrid.WorldToCell(transform.position);
            //GameObject chest = cellPos.
            //GetComponent<BlackBoard>().RemovePOI(TileType.TREASURE, chest);
        }
        else if (item.GetType() == typeof(Potion)) {
            Health += ((Potion)item).value;
        }
    }

    public void InteractionRange(GameObject target) {
        if (target.GetComponent<Enemy>()) {

        }
    }
}

