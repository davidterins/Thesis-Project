using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour {

    Text label;

    private void Start() {
        label = GetComponent<Text>();
    }

    public void UpdateText(float value) {     
        label.text = "" + value / 100;
    }
}
