using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class FileBrowserAR : MonoBehaviour
{
    [SerializeField] GameObject _mainButtonsParentObject;
    [SerializeField] GameObject _secondaryButtonsParentObject;
    [SerializeField] TMP_InputField _directoryText;
    [SerializeField] GameObject _fileButtonPrefab;

    List<GameObject> primaryButtons = new List<GameObject>();
    List<GameObject > secondaryButtons = new List<GameObject>();

    void CreateButtonForDirectory(string directory, FileButtonType buttonType, string buttonName, bool isEnableInteraction)
    {
        GameObject newButton = Instantiate(_fileButtonPrefab, buttonType == FileButtonType.Primary ? _mainButtonsParentObject.transform : _secondaryButtonsParentObject.transform);
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonName;
        if(isEnableInteraction)
        {
            newButton.GetComponent<UnityEngine.UI.Toggle>().onValueChanged.AddListener((isOn) =>
            {
                _directoryText.text = directory;
                InitializeSecondaryButtons();
            });
        }
        else
        {
            newButton.GetComponent<UnityEngine.UI.Toggle>().interactable = false;
        }

        if (buttonType == FileButtonType.Primary)
        {
            primaryButtons.Add(newButton);
        }
        else
        {
            secondaryButtons.Add(newButton);
        }
    }
    void CreateButtonForDirectory(Environment.SpecialFolder directory, FileButtonType buttonType, string buttonName, bool isEnableInteraction)
    {
        GameObject newButton = Instantiate(_fileButtonPrefab, buttonType == FileButtonType.Primary ? _mainButtonsParentObject.transform : _secondaryButtonsParentObject.transform);
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonName;
        if (isEnableInteraction)
        {
            newButton.GetComponent<UnityEngine.UI.Toggle>().onValueChanged.AddListener((isOn) =>
            {
                _directoryText.text = Environment.GetFolderPath(directory);

                InitializeSecondaryButtons();

            });
        }
        else
        {
            newButton.GetComponent<UnityEngine.UI.Toggle>().interactable = false;
        }

        if (buttonType == FileButtonType.Primary)
        {
            primaryButtons.Add(newButton);
        }
        else
        {
            secondaryButtons.Add(newButton);
        }
    }
    void CreateBackButton()
    {
        GameObject newButton = Instantiate(_fileButtonPrefab, _mainButtonsParentObject.transform);
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = "Назад";
        newButton.GetComponent<UnityEngine.UI.Toggle>().onValueChanged.AddListener((isOn) => 
        {
            if (!string.IsNullOrEmpty(_directoryText.text) && Directory.Exists(_directoryText.text))
            {
                string newPath = "";
                try
                {
                    newPath = Directory.GetParent(_directoryText.text).FullName;
                }catch{}

                if (!string.IsNullOrEmpty(newPath) && Directory.Exists(newPath)) _directoryText.text = newPath;
                InitializeSecondaryButtons();
               
            }
        });
        primaryButtons.Add(newButton);
    }
    void InitializeMainButtons()
    {
        CreateBackButton();
        CreateButtonForDirectory(Environment.SpecialFolder.MyDocuments, FileButtonType.Primary, "Документы", true);
        CreateButtonForDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), FileButtonType.Primary, "Рабочий стол",true);

        foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
        {
            CreateButtonForDirectory(driveInfo.Name, FileButtonType.Primary, driveInfo.Name, true);
        }

    }
    void InitializeSecondaryButtons()
    {
        ClearSecondaryButtons();

        if (!string.IsNullOrEmpty(_directoryText.text) && Directory.Exists(_directoryText.text))
        {
            foreach (string dir in Directory.GetDirectories(_directoryText.text))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dir);
                CreateButtonForDirectory(dir, FileButtonType.Secondary, "[] " + dirInfo.Name,true);
            }
            foreach (string file in Directory.GetFiles(_directoryText.text))
            {
                FileInfo fileInfo = new FileInfo(file);
                CreateButtonForDirectory(Path.GetFileName(fileInfo.FullName), FileButtonType.Secondary, fileInfo.Name,false);
            }
        }
    }
    enum FileButtonType
    {
        Primary, Secondary
    }

    void ClearSecondaryButtons()
    {
        foreach (GameObject button in secondaryButtons)
        {
            Destroy(button);        
        }
        secondaryButtons.Clear();
    }
    void ClearPrimaryButtons()
    {
        foreach (GameObject button in primaryButtons)
        {
            Destroy(button);
        }
        primaryButtons.Clear();
    }
    void ClearButtons()
    {
        ClearPrimaryButtons();
        ClearSecondaryButtons();
    }
    string GetParentDirectory(string currentDirectory)
    {
        DirectoryInfo parentDir = Directory.GetParent(currentDirectory);
        if (parentDir != null)
            return parentDir.FullName;
        else
            return "";
    }

    private void OnEnable()
    {
        InitializeMainButtons();
    }
    private void OnDisable()
    {
        ClearButtons();
    }
}
