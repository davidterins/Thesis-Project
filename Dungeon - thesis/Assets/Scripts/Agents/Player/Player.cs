using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
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

  public Stack<Potion> potions;

  public Stack<Key> Keys;

  public int Coins = 0;

  public Key Key { get { return Keys.Peek(); } }


  private void Start()
  {
    Keys = new Stack<Key>();
    potions = new Stack<Potion>();
    weapons = new List<Weapon>();
    EquipWeapon(currentWeapon.GetComponent<Weapon>());
  }


  //TODO Denna funktionaliteten ska in Inventory.cs
  public override void HandlePickup(Item item)
  {
    if (item.GetType() == typeof(Coin))
    {
      Coins += ((Coin)item).value;
      InfoBox.coins = Coins;
    }
    else if (item.GetType() == typeof(Potion))
    {
      GetComponent<Inventory>().AddItem(item);
      potions.Push((Potion)item);
      if (potions.Count > 0)
      {
        GetComponent<BlackBoard>().HasPotion = true;
      }
    }
    else if (item.GetType() == typeof(Weapon))
    {
      GetComponent<Inventory>().AddItem(item);
      weapons.Add((Weapon)item);
    }
    else if (item.GetType() == typeof(Key))
    {
      GetComponent<Inventory>().AddItem(item);
      Keys.Push((Key)item);
      if (Keys.Count > 0)
      {
        GetComponent<BlackBoard>().HasKey = true;
      }
    }
  }

  public void EquipWeapon(Weapon weapon)
  {
    switch (weapon.Type)
    {
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

  //TODO Denna funktionaliteten ska in Inventory.cs
  public void UseItem(string itemType)
  {
    if (itemType == "Key")
      Keys.Pop().Use();
    GetComponent<Inventory>().TryGetItem(typeof(Key));
    if (Keys.Count == 0)
    {
      GetComponent<BlackBoard>().HasKey = false;
    }

    if (itemType == "Potion")
    {
      //Health += potions.Peek().value;
      ModifyHealth(potions.Peek().value);
      GetComponent<Inventory>().TryGetItem(typeof(Potion));
      potions.Pop().Use();

      //if (Health > (MaxHealth / 2) + 1)
      //GetComponent<BlackBoard>().IsHealthy = true;
      if (potions.Count == 0)
      {
        GetComponent<BlackBoard>().HasPotion = false;
      }
      InfoBox.hp = Health;

    }

  }

  public override void TakeDamage(GameObject attacker, int amount)
  {
    base.TakeDamage(attacker, amount);
    //if (Health <= MaxHealth / 2 -1)
    //GetComponent<BlackBoard>().IsHealthy = false;
    InfoBox.hp = Health;
  }

  protected override void HandleDeath(GameObject attacker)
  {
    //TODO Göra något när spelaren dör.
  }
}

