using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Add hashing to Dictionary List

/// <summary>
/// Black board.
/// Tanken är att agentens minne ska finnas här i en massa properties.
/// </summary>
public class BlackBoard : MonoBehaviour {
    TargetingService targetingService;
    [SerializeField]
    Persona currentPersona;

    public Dictionary<TileType, List<GameObject>> Memory { get; private set; }

    public void Start() {
        targetingService = new TargetingService(gameObject);
        if (!currentPersona)
            gameObject.AddComponent<DefaultPersona>();

        Memory = new Dictionary<TileType, List<GameObject>>() {
            { TileType.ENEMY, new List<GameObject>() },
            { TileType.TREASURE, new List<GameObject>() },
            { TileType.DOOR, new List<GameObject>() },
            { TileType.DOORENTER, new List<GameObject>() }
        };
    }

    public void AddPOI(TileType type, GameObject go) {
        if (type != TileType.FLOOR)
            try {
                if (!Memory[type].Contains(go)) {
                    Memory[type].Add(go);
                    InfoBox.UpdateMemory(Memory);
                    Debug.Log("Added " + type + " count: " + Memory[type].Count);
                }
            }
            catch (System.Exception ex) {
                Debug.Log(ex.Message + "Type = " + type);
            }
    }

    public void RemovePOI(TileType type, GameObject go) {
        if (Memory[type].Contains(go)) {
            Memory[type].Remove(go);
            InfoBox.UpdateMemory(Memory);
        }
    }

    public float GetAttackEnemyRelevancy() {
        if (Memory[TileType.ENEMY].Count <= 0)
            return 0f;
        return currentPersona.AttackEnemyFactor(targetingService.TryGetEnemyTarget());
    }

    public float GetLootTreasureRelevancy() {
        if (Memory[TileType.TREASURE].Count <= 0)
            return 0f;
        return currentPersona.LootTreasureFactor();
    }

    public GameObject EnemyObject { get { return targetingService.TryGetEnemyTarget(); } }

    public GameObject TreasureObject { get { return targetingService.TryGetTreasureChest(); } }

    public Vector2 LootPosition { get; set; }

    public int Health { get { return GetComponent<Player>().Health; } }

    public int Coins { get { return GetComponent<Player>().Coins; } }

}
