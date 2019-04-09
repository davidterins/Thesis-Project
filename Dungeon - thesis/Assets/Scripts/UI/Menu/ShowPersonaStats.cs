using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPersonaStats : MonoBehaviour {

    public GameObject customPersonaPanel;
    public GameObject statPanel;

    /// <summary>
    /// Shows the stats of the selected persona, or shows menu where you can customize your own.
    /// </summary>
    /// <param name="chosenValue"></param>
    public void ShowStats(int chosenValue) {
        switch (chosenValue) {
            case 3:
                customPersonaPanel.SetActive(true);
                break;
            default:
                customPersonaPanel.SetActive(false);
                break;
        }
    }
}
