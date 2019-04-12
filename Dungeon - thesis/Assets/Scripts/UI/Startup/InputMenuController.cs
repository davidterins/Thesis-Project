using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;

public class InputMenuController : MonoBehaviour
{
  [SerializeField]
  ActionButton RunButton = null;

  [SerializeField]
  ActionButton OpenFileButton = null;

  [SerializeField]
  InputField FilePathInput = null;

  [SerializeField]
  Text PersonaDropDownText = null;

  [SerializeField]
  Dropdown PersonaDropDown = null;

  [SerializeField]
  Text DefaultMapDropDownText = null;

  [SerializeField]
  Dropdown DefaultMapDropDown = null;

  private void Awake()
  {
    OpenFileButton.AssignAction(OpenFileMenu);
    RunButton.AssignAction(Run);

    FilePathInput.onValueChanged.AddListener((input) =>
    {
      if(input.Length > 0)
      {
        DefaultMapDropDown.interactable = false;
        return;
      }
      DefaultMapDropDown.interactable = true;
    });

    PersonaDropDown.onValueChanged.AddListener(HandlePersonaDropDownChanged);

  }

  void HandlePersonaDropDownChanged(int index)
  {
    switch (PersonaDropDownText.text)
    {
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
        for (int i = 0; i < modifiers.Length; i++)
        {
          modifiers[i] = customModifiersPanel.GetChild(i).Find("Slider").GetComponent<Slider>().value / 100f;
        }
        Settings.SetPersona(Settings.Persona.Custom);
        break;
      default:
        Settings.SetPersona(Settings.Persona.Default);
        break;
    }
    Settings.SetSpeed(Mathf.RoundToInt(transform.Find("SpeedInputRow").transform.Find("Slider").GetComponent<Slider>().value));
  }


  void OpenFileMenu()
  {
    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

    string mapPath = EditorUtility.OpenFilePanel("Open Map", path, "map");

    FilePathInput.text = mapPath;

    Debug.Log("Selected map " + mapPath);
   
  }


  void Run()
  {

    string selectedMapFile = "";
    if(DefaultMapDropDown.interactable)
    {
      selectedMapFile = Path.Combine(Application.dataPath, "Maps/" + DefaultMapDropDownText.text);
    }
    else if(FilePathInput.text.EndsWith(".map"))
    {
      selectedMapFile = FilePathInput.text;
    }
    else
    {
      return;
    }
   
    Settings.SelectedMapFromMenu = selectedMapFile;
    Settings.GameStartedFromMenu = true;
    SceneManager.LoadScene("DungeonScene");

  }
}
