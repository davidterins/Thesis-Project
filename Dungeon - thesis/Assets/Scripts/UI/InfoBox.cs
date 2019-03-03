using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InfoBox {
    public static string targetTile = "";
    public static string playerTile = "";
    public static int pathLength = 0;
    public static int stepsLeft = 0;
    private static string memory = "";
    public static int hp = 100;
    public static int coins;

    public static void AddToMemory(string item) {
        memory += ", " + item;
    }

    public static string GetMemory() {
        return memory;
    }
}
