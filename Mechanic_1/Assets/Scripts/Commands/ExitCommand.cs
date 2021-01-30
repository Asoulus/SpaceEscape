using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Exit Command", menuName = "Commands/ExitCommands")]
public class ExitCommand : Command
{
    public override bool Process(string[] args)
    {
        if (args.Length > 0)
        {
            WrongAmmountOfArguments();
        }
        else
        {              
            InputEventHandler.instance.ConsoleExit();
        }

        return true;
    }

}
