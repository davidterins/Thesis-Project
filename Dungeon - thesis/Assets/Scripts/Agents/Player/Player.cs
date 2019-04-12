using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
  public static event Action OnPlayerDeath = delegate { };

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


  private void Start()
  {

    weapons = new List<Weapon>();
    EquipWeapon(currentWeapon.GetComponent<Weapon>());

    //GetComponent<BlackBoard>().Health = Health;
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
    //TODO Göra något när spelaren dör. Kanske visa statsen?
    OnPlayerDeath.Invoke();
    Destroy(gameObject);
  }

  public override void ModifyHealth(int amount)
  {
    base.ModifyHealth(amount);
    // GetComponent<BlackBoard>().Health = Health;
  }
}

