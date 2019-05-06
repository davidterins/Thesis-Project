using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
  public enum Persona { Rusher, TreasureHunter, Explorer, MonsterSlayer, Custom, Default, }
  static Persona currentPersona;
  static float[] customModifiers;
  static float agentSpeed = 0.8f, walkSpeed = 0.08f;
  public static bool GameStartedFromMenu;
  public static int Iterations = 1;

  public static string SelectedMapFromMenu;

  public static void SetPersona(Persona persona, float[] modifiers = null)
  {
    currentPersona = persona;
    if (modifiers != null)
      customModifiers = modifiers;
  }

  public static Persona FetchPersona()
  {
    return currentPersona;
  }

  public static float[] FetchCustomModifiers()
  {
    return customModifiers;
  }

  public static void SetSpeed(int speed)
  {
    switch (speed)
    {
      case 1:
        agentSpeed = 0.85f;
        walkSpeed = 0.08f;
        break;
      case 2:
        agentSpeed = 0.35f;
        walkSpeed = 0.1f;
        break;
      case 3:
        agentSpeed = 0.05f;
        walkSpeed = 0.15f;
        break;
      case 4:
        agentSpeed = 0.0f; // TODO: Make instant calculations without showing the scene itself.
        walkSpeed = 0.2f;
        break;
      default:
        break;
    }
  }

  public static float FetchSpeed()
  {
    return agentSpeed;
  }

  public static float FetchWalkSpeed()
  {
    return walkSpeed;
  }
}
