using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedSliderValue : MonoBehaviour {

    Text label;

    private void Start() {
        label = GetComponent<Text>();
    }

    public void UpdateText(float value) {
        switch (Mathf.RoundToInt(value)) {
            case 1:
                label.text = "Slow";
                break;
            case 2:
                label.text = "Medium";
                break;
            case 3:
                label.text = "Fast";
                break;
            case 4:
                label.text = "Instant";
                break;
            default:
                label.text = "?";
                break;
        }
    }    
}
