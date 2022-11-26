using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Clear Command", menuName = "Commands/ClearCommands")]
public class ClearCommand : Command
{
    public override bool Process(string[] args)
    {
        if (args.Length > 0)
        {
            WrongAmmountOfArguments();
        }
        else
        {
            PlayerInputHandler.instance.ClearConsole();    
        }

        return true;
    }
}
