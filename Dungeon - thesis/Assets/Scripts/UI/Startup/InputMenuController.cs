using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEditor;
using UnityEngine.SceneManagement;

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
  GameObject ImportInfo = null;


  private void Awake()
  {
    OpenFileButton.AssignAction(OpenFileMenu);
    RunButton.AssignAction(Run);
    //TODO Assigna Run button.

  }

  void OpenFileMenu()
  {
    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    string mapPath = EditorUtility.OpenFilePanel("Open Map", path, "map");

    FilePathInput.text = mapPath;

    Debug.Log("Selected map " + mapPath);
   
  }


  void Run()
  {

    if(!FilePathInput.text.EndsWith(".map"))
    {
      return;
    }

    ImportInfo.GetComponent<ImportInfo>().DungeonFilePath = FilePathInput.text;
    SceneManager.LoadScene("DungeonScene");

   //Application.DontDestroyOnLoad();
  }

}
