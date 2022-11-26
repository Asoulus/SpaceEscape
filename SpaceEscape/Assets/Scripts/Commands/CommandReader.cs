using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandReader  : MonoBehaviour
{
    public static CommandReader instance;

    [Header("Commands")]
    [SerializeField]
    private List<Command> _allCommands = new List<Command>();

    private List<string> _allCommandWords = new List<string>();


    public List<string> AllCommandWords 
    {
        get => _allCommandWords;
    }
    
    public List<Command> AllCommands
    {
        get => _allCommands;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        foreach (var command in _allCommands)
        {
            _allCommandWords.Add(command.CommandWord);
        }
    }

    public void ReadInput()
    {
        string commandLine = PlayerInputHandler.instance.PlayerInput.text;

        PlayerInputHandler.instance.SaveToInputHistory(commandLine);

        if (commandLine == string.Empty)
            return;

        string[] inputSplit = commandLine.Split(' ');      

        string commandWord = inputSplit[0];      

        string[] args = inputSplit.Skip(1).ToArray();

        SaveToHistory(commandLine);
     
        IEnumerable<ICommand> tmpCommandList = PlayerInputHandler.instance.CurrentFolder.MyCommands;

        List<ICommand> tmpList = tmpCommandList.ToList();

        bool foundOnObject = false;
        bool foundOverall = false;

        for (int i = 0; i < _allCommandWords.Count; i++)
        {
            if (string.Equals(_allCommandWords[i], commandWord))
            {
                foundOverall = true;
                for (int j = 0; j < tmpList.Count; j++)
                {
                    if (string.Equals(tmpList[j].CommandWord, commandWord))
                    {
                        PlayerInputHandler.instance.CurrentFolder.PerformCommandAction(commandWord, args);
                        foundOnObject = true;
                        break;
                    }
                }
            }      
        }
        CommandDoesntExist(foundOverall, foundOnObject, commandWord);
    }

    private void CommandDoesntExist(bool overall, bool onObject, string command)
    {
        if (!overall)
        {
            string tmp = "Command: <color=green>" + command + "</color> doesn't exist\n";
            PlayerInputHandler.instance.ConsoleHistory.text += tmp;
        }
        else if(!onObject)
        {
            string tmp = "Object Doesnt Contain " + command + " command\n";//TODO potencjalna zmiana
            PlayerInputHandler.instance.ConsoleHistory.text += tmp;
        }
    }

    private void SaveToHistory(string message)
    {
        string tmpMessage = PlayerInputHandler.instance.CurrentDirectory + "<color=green>" + message + "</color>\n";
        PlayerInputHandler.instance.ConsoleHistory.text += tmpMessage;
        PlayerInputHandler.instance.PlayerInput.text = string.Empty;

        if (message == "exit")
        {
            return;
        }
        else
        {
            PlayerInputHandler.instance.PlayerInput.ActivateInputField();
        }
        
    }
}
