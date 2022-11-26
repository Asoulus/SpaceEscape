using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler instance;

    [Header("References")]
    [SerializeField]
    private TMP_Text _consoleHistory = null;
    [SerializeField]
    private TMP_InputField _playerInput = null;
    [SerializeField]
    private PCscript _currentPC = null; 
    [SerializeField]
    private Scrollbar _currentScroll = null;

    [Header("Current Folder")]
    private IFolder _currentFolder;
    public GameObject _currentFolderGO = null;

    private List<string> _inputHistory = new List<string>();

    [SerializeField]
    private int _historyIndex = 0;

    [Header("Current Directory")]
    [SerializeField]
    private string _currentDirectory;
    [SerializeField]
    private TMP_Text _directory = null; 
    [SerializeField]
    private TMP_Text _inputText = null;


    public string CurrentDirectory
    {
        get => _currentDirectory;
    }

    public int HistoryIndex
    {
        set => _historyIndex = value;
    }

    public TMP_InputField PlayerInput
    {
        get => _playerInput;
        set => _playerInput = value;
    }

    public TMP_Text ConsoleHistory
    {
        get => _consoleHistory;
        set => _consoleHistory = value;
    }
    public PCscript CurrentPC
    {
        get => _currentPC;
        set => _currentPC = value;
    }

    public IFolder CurrentFolder
    {
        get => _currentFolder;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InputEventHandler.instance.onScrollWheelUsed += ScrollWheelUsed;
        InputEventHandler.instance.onArrowUpPressed += ArrowUpPressed;
        InputEventHandler.instance.onArrowDownPressed += ArrowDownPressed;
    }

    private void ArrowDownPressed()
    {
     
        if (_playerInput != null)
        {
            if (_historyIndex < _inputHistory.Count - 1)
            {
                _historyIndex++;
                Preview();
            }
        }      
    }

    private void ArrowUpPressed()
    {
        if (_playerInput != null)
        {
            if (_historyIndex > 0)
            {
                _historyIndex--;
                Preview();
            }
        }   
    }

    private void Preview()
    {
        _playerInput.text = _inputHistory[_historyIndex];
    }

    private void ScrollWheelUsed(float value)
    {
        if (_currentScroll != null)
        {
            _currentScroll.value += (value / 10);
        }     
    }

    public void DisplayCommands(string value)
    {
        string tmp = value + "\n\n";
        _consoleHistory.text += tmp;
    }

    public void SaveToInputHistory(string text)
    {
        _inputHistory.Add(text);
        _historyIndex = _inputHistory.Count;
    }

    public void ClearConsole()
    {
        if (_consoleHistory != null)
        {
            _consoleHistory.text = "Type <color=green>help</color> to see list of aviable commands\nType <color=green>help command</color> to see the discription of the <color=green>command</color>\n\n\n";
        }
    }

    public void AssignCurrentPC(PCscript pc)
    {
        _currentPC = pc;
        ConsoleHistory = pc._myHistory;
        PlayerInput = pc._myInput;
        _currentScroll = pc._myScroll;

        AssignCurrentFolder(pc.gameObject);
    }

    public void AssignCurrentFolder(GameObject folderObject)
    {
        _currentFolderGO = folderObject;
        _currentFolder = folderObject.GetComponent<IFolder>();

        string directory = string.Empty;

        if (_currentFolderGO.GetComponent<PCscript>())
        {
            directory = CurrentPC._myFileName;
        }
        else if (_currentFolderGO.GetComponent<Device>())
        {
            directory = CurrentPC._myFileName + _currentFolderGO.GetComponent<Device>().MyFileName;
        }

        directory += ">";
        AssignCurrentDirectory(directory);
    }

    private void AssignCurrentDirectory(string directory)
    {
        _directory = CurrentPC._myDirectory;
        _directory.text = directory;
        _currentDirectory = directory;
        _inputText = CurrentPC._myInputText;
        

        ResizeDirectory();
    }

    public void ResizeDirectory()
    {
        RectTransform tmpInputField = _playerInput.GetComponent<RectTransform>();

        float tmpWidth = 1880 - _directory.text.Length * 45;

        tmpInputField.sizeDelta = new Vector2(tmpWidth, 62);
 }

    private void OnDisable()
    {
        InputEventHandler.instance.onScrollWheelUsed -= ScrollWheelUsed;
        InputEventHandler.instance.onArrowUpPressed -= ArrowUpPressed;
        InputEventHandler.instance.onArrowDownPressed -= ArrowDownPressed;
    }


}
