using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "New Help Command", menuName = "Commands/HelpCommands")]
public class HelpCommand : Command
{
    
    public override bool Process(string[] args)
    {
        if (args.Length == 0)
        {
            ObjectAviableCommands();
        }
        else if (args.Length == 1)
        {
            CommandExplenation(args);
        }
        else
        {
            WrongAmmountOfArguments();
        }
        return true;
    }

    private void ObjectAviableCommands()
    {
        IEnumerable<ICommand> commands;

        commands = PlayerInputHandler.instance.CurrentFolder.MyCommands;

        List<string> commandWordsList = new List<string>();


        foreach (var command in commands)
        {
            string tmp;
            tmp = command.CommandWord;
            commandWordsList.Add(tmp);
        }

        string message = "Aviable Commands: <color=green>\n";
        message += string.Join("\n", commandWordsList);
        message += "</color>";

        PlayerInputHandler.instance.DisplayCommands(message);
    }

    private void CommandExplenation(string[] args)
    {
        string message = string.Empty;
        bool checkForExistance = false;
        Command tmpCommand = null;
        
        foreach (var command in CommandReader.instance.AllCommands)
        {
            if (args[0].Equals(command.CommandWord))
            {
                checkForExistance = true;              
                tmpCommand = command;
                break;
            }
            else
            {
                checkForExistance = false;
                
            }
        }

        if (checkForExistance)
        {
            message = tmpCommand.Discription;
        }
        else
        {
            message = "Command <color=green>" + args[0] + "</color> doesn't exist";
        }

        //string message = "Explaining command: <color=green>" + args[0] + "</color>";
        PlayerInputHandler.instance.DisplayCommands(message);
    }
}

