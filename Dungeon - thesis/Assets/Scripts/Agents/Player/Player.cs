using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;

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

  public override void HandlePickup(Item item)
  {
    if (item.GetType() == typeof(Coin))
    {
      Coins += ((Coin)item).value;
    }
    else if (item.GetType() == typeof(Potion))
    {
      potions.Push((Potion)item);
      if (potions.Count > 0)
      {
        GetComponent<BlackBoard>().HasPotion = true;
      }
    }
    else if (item.GetType() == typeof(Weapon))
    {
      weapons.Add((Weapon)item);
    }
    else if (item.GetType() == typeof(Key))
    {
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

  public void UseItem(string itemType)
  {
    if (itemType == "Key")
      Keys.Pop().Use();

    if (itemType == "Potion")
    {
      Health += potions.Peek().value;
      potions.Pop().Use();
      InfoBox.hp = Health;

    }

  }

  public override void TakeDamage(GameObject attacker, int amount)
  {
    base.TakeDamage(attacker, amount);
    InfoBox.hp = Health;
  }

  protected override void HandleDeath(GameObject attacker)
  {
    //base.HandleDeath(attacker);
  }
}

