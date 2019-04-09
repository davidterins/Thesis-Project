using System.Collections.Generic;
using UnityEngine;

public class Player : Agent {
    [SerializeField]
    private GameObject currentWeapon = null;

    [SerializeField]
    Sprite meleeEquipedSprite = null;

    [SerializeField]
    Sprite rangedEquipedSprite = null;

    [SerializeField]
    Sprite defaultSprite = null;

    [SerializeField]
    public List<Weapon> weapons;

    public int Coins = 0;

    public static float LowHealthLimit = 0.4f;

    // public static float HighHealthLimit = 0.8f;


    private void Start() {
        Destroy(GetComponent<DefaultPersona>());
        switch (Settings.FetchPersona()) {
            case Settings.Persona.Custom:
                gameObject.AddComponent<CustomPersona>();
                gameObject.GetComponent<CustomPersona>().SetValues(Settings.FetchCustomModifiers());
                InfoBox.persona = "Custom";
                break;
            case Settings.Persona.Explorer:
                //gameObject.AddComponent<ExplorerPersona>();
                InfoBox.persona = "Explorer";
                break;
            case Settings.Persona.MonsterSlayer:
                gameObject.AddComponent<MonsterSlayerPersona>();
                InfoBox.persona = "Monster Slayer";
                break;
            case Settings.Persona.Rusher:
                gameObject.AddComponent<RusherPersona>();
                InfoBox.persona = "Rusher";
                break;
            case Settings.Persona.TreasureHunter:
                gameObject.AddComponent<TreasureHunterPersona>();
                InfoBox.persona = "Treasure Hunter";
                break;
            default:
                gameObject.AddComponent<DefaultPersona>();
                InfoBox.persona = "Default";
                break;
        }
        weapons = new List<Weapon>();
        EquipWeapon(currentWeapon.GetComponent<Weapon>());

        //GetComponent<BlackBoard>().Health = Health;
    }


    public void PickupItem(Item item) {
        if (item.GetType() == typeof(Coin)) {
            Coins += ((Coin)item).value;
            InfoBox.coins = Coins;
        }
        else {
            GetComponent<Inventory>().AddItem(item);
        }
    }

    public void EquipWeapon(Weapon weapon) {
        switch (weapon.Type) {
            case WeaponType.Melee:
                GetComponent<SpriteRenderer>().sprite = meleeEquipedSprite;
                break;
            case WeaponType.Range:
                GetComponent<SpriteRenderer>().sprite = rangedEquipedSprite;
                break;
            default:
                GetComponent<SpriteRenderer>().sprite = defaultSprite;
                break;
        }
        var equipedWeapon = currentWeapon.GetComponent<Weapon>();
        equipedWeapon = weapon;
    }


    public override void TakeDamage(GameObject attacker, int amount) {
        base.TakeDamage(attacker, amount);
        InfoBox.hp = Health;
    }

    protected override void HandleDeath(GameObject attacker) {
        //TODO Göra något när spelaren dör. Kanske visa statsen?
    }

    public override void ModifyHealth(int amount) {
        base.ModifyHealth(amount);
        // GetComponent<BlackBoard>().Health = Health;
    }
}

