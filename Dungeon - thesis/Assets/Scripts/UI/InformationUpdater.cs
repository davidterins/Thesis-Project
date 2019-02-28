using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationUpdater : MonoBehaviour {
    private Text infoText;

    public void Start() {
        infoText = transform.GetChild(0).GetComponent<Text>();
    }

    public void Update() {
        infoText.text = "Information:\nPlayer pos: " + InfoBox.playerTile + "\nTarget pos: " + InfoBox.targetTile + "\nPath length: " + InfoBox.pathLength + "\nSteps left: " + InfoBox.stepsLeft + "\nHP: " + InfoBox.hp;
    }
}
