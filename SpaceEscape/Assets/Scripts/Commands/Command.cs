using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : ScriptableObject, ICommand
{
    [SerializeField] 
    private string _commandWord = string.Empty;
    [Multiline]
    [SerializeField] 
    private string _discription = string.Empty;

    public string CommandWord => _commandWord;

    public string Discription
    {
        get => _discription;
    }

    public abstract bool Process(string[] args);

    public void WrongAmmountOfArguments()
    {
        string message = "Wrong ammount of arguments";
        PlayerInputHandler.instance.DisplayCommands(message);
    }

}
