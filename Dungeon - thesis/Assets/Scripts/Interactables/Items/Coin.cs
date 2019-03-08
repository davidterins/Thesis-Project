using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item {

    [SerializeField]
    public readonly int value = 10;

    public override ItemType Type {
        get {
            return ItemType.Coin;
        }
    }

    public void Start() {
        InfoBox.coins += value;
    }
}
