using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour {
    bool IsClosed = true;

    [SerializeField]
    GameObject[] Loot = null;

    void Start() {}

    void Open(GameObject interactingAgent) {
        foreach (var item in Loot) {
            Instantiate(item, transform);
        }
        IsClosed = false;
        try {
            interactingAgent.GetComponent<BlackBoard>().RemovePOI(TileType.TREASURE, this.gameObject);
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
        catch { Debug.Log("Agent has no memory applied."); }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (IsClosed)
            if (collision.gameObject.CompareTag("Player")) {
                Open(collision.gameObject);
                Debug.Log(" Player collided with chest");
            }
    }
}
