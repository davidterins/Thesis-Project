using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

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

  public List<Potion> potions;

  public int Coins = 0;
 

  private void Start()
  {
    potions = new List<Potion>();
    weapons = new List<Weapon>();
    EquipWeapon(currentWeapon.GetComponent<Weapon>());
  }

  public override void HandlePickup(Item item)
  {
    if (item.GetType() == typeof(Coin))
    {
      Coins += ((Coin)item).value;
      //var cellPos = GameObject.FindWithTag("Dungeon").GetComponent<Dungeon>().WorldGrid.WorldToCell(transform.position);
      //GameObject chest = cellPos.
      //GetComponent<BlackBoard>().RemovePOI(TileType.TREASURE, chest);
    }
    else if (item.GetType() == typeof(Potion))
    {
      Health += ((Potion)item).value;
    }
    else if (item.GetType() == typeof(Weapon))
    {
      weapons.Add((Weapon)item);
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


}

