using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;
using UnityEngine.SceneManagement;

public class InputMenuController : MonoBehaviour {
    [SerializeField]
    ActionButton RunButton = null;

    [SerializeField]
    ActionButton OpenFileButton = null;

    [SerializeField]
    InputField FilePathInput = null;

    [SerializeField]
    Text PersonaDropDownText = null;

    [SerializeField]
    GameObject ImportInfo = null;

    private void Awake() {
        OpenFileButton.AssignAction(OpenFileMenu);
        RunButton.AssignAction(Run);
        //TODO Assigna Run button.

    }

    void OpenFileMenu() {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        string mapPath = EditorUtility.OpenFilePanel("Open Map", path, "map");

        FilePathInput.text = mapPath;

        Debug.Log("Selected map " + mapPath);

    }


    void Run() {

        if (!FilePathInput.text.EndsWith(".map")) {
            return;
        }        

        switch (PersonaDropDownText.text) {
            case "Rusher":
                Settings.SetPersona(Settings.Persona.Rusher);
                break;
            case "Treasure Hunter":
                Settings.SetPersona(Settings.Persona.TreasureHunter);
                break;
            case "Monster Slayer":
                Settings.SetPersona(Settings.Persona.MonsterSlayer);
                break;
            case "Custom":                
                Transform customModifiersPanel = transform.parent.transform.Find("CustomPersonaPanel").transform.Find("SliderPanel");
                float[] modifiers = new float[customModifiersPanel.childCount];
                for (int i = 0; i < modifiers.Length; i++) {
                    modifiers[i] = customModifiersPanel.GetChild(i).Find("Slider").GetComponent<Slider>().value / 100f;
                }
                Settings.SetPersona(Settings.Persona.Custom);
                break;
            default:
                Settings.SetPersona(Settings.Persona.Default);
                break;
        }
        Settings.SetSpeed(Mathf.RoundToInt(transform.Find("SpeedInputRow").transform.Find("Slider").GetComponent<Slider>().value));

        ImportInfo.GetComponent<ImportInfo>().DungeonFilePath = FilePathInput.text;
        SceneManager.LoadScene("DungeonScene");

        //Application.DontDestroyOnLoad();
    }
}
