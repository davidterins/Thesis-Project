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

  public int Coins = 0;

  private void Start()
  {
    weapons = new List<Weapon>();
    EquipWeapon(currentWeapon.GetComponent<Weapon>());
  }


  public void PickupItem(Item item)
  {
    if (item.GetType() == typeof(Coin))
    {
      Coins += ((Coin)item).value;
      InfoBox.coins = Coins;
    }
    else
    {
      GetComponent<Inventory>().AddItem(item);
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


  public override void TakeDamage(GameObject attacker, int amount)
  {
    base.TakeDamage(attacker, amount);
    InfoBox.hp = Health;
  }

  protected override void HandleDeath(GameObject attacker)
  {
    //TODO Göra något när spelaren dör.
  }
}

