using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityVolumeRendering.RuntimeFileBrowser.RuntimeFileBrowserComponent;

public class FileBrowserAR : MonoBehaviour
{
    [SerializeField] GameObject _mainButtonsParentObject;
    [SerializeField] GameObject _secondaryButtonsParentObject;
    [SerializeField] TMP_InputField _directoryText;
    [SerializeField] GameObject _fileButtonPrefab;

    List<GameObject> primaryButtons = new List<GameObject>();
    List<GameObject > secondaryButtons = new List<GameObject>();    

    void CreateButtonForDirectory(string directory, FileButtonType buttonType, string buttonName)
    {
        GameObject newButton = Instantiate(_fileButtonPrefab, buttonType == FileButtonType.Primary ? _mainButtonsParentObject.transform : _secondaryButtonsParentObject.transform);
        
        newButton.GetComponent<UnityEngine.UI.Toggle>().onValueChanged.AddListener((isOn) =>
        {
            _directoryText.text = directory;

        });
    }
    void CreateButtonForDirectory(Environment.SpecialFolder directory, FileButtonType buttonType, string buttonName)
    {

        GameObject newButton = Instantiate(_fileButtonPrefab, buttonType == FileButtonType.Primary ? _mainButtonsParentObject.transform : _secondaryButtonsParentObject.transform);
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonName;
        newButton.GetComponent<UnityEngine.UI.Toggle>().onValueChanged.AddListener((isOn) =>
        {
            _directoryText.text = Environment.GetFolderPath(directory);

            InitializeSecondaryButtons();

        });
        if (buttonType == FileButtonType.Primary)
        {
            primaryButtons.Add(newButton);
        }
        else
        {
            secondaryButtons.Add(newButton);
        }
    }

    void InitializeMainButtons()
    {
        CreateButtonForDirectory(Environment.SpecialFolder.MyDocuments, FileButtonType.Primary, "Documents");
    }
    void InitializeSecondaryButtons()
    {
        ClearSecondaryButtons();


        if (!string.IsNullOrEmpty(_directoryText.text) && Directory.Exists(_directoryText.text))
        {
            //scrollPos = GUILayout.BeginScrollView(scrollPos);
            // Draw directories
            foreach (string dir in Directory.GetDirectories(_directoryText.text))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dir);
                //if (GUILayout.Button(dirInfo.Name))
                //{
                //    _directoryText.text = dir;
                //}
                CreateButtonForDirectory(dir, FileButtonType.Secondary, dirInfo.Name);
            }
            // Draw files
            //if (dialogMode == DialogMode.OpenFile || dialogMode == DialogMode.SaveFile)
            //{

            //}
            //foreach (string file in Directory.GetFiles(_directoryText.text))
            //{
            //    FileInfo fileInfo = new FileInfo(file);
            //    if (GUILayout.Button(fileInfo.Name))
            //    {
            //        selectedFile = fileInfo.FullName;
            //        fileName = Path.GetFileName(selectedFile);
            //    }
            //}
            //GUILayout.EndScrollView();
        }
        //GUILayout.EndVertical();
    }
    enum FileButtonType
    {
        Primary, Secondary
    }
    private void Start()
    {
        InitializeMainButtons();
    }

    void ClearSecondaryButtons()
    {
        foreach (GameObject button in secondaryButtons)
        {
            Destroy(button);        
        }
        secondaryButtons.Clear();
    }
}
