using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LoopCommand", menuName = "Commands/LoopCommands")]

public class LoopCommand : Command
{
    public override bool Process(string[] args)
    {
        if (args.Length != 1)
        {
            WrongAmmountOfArguments();
        }
        else
        {
            int tmpNumber;
            if (int.TryParse(args[0], out tmpNumber))
            {
                if (tmpNumber < 0 && tmpNumber > 10)
                {
                    string message = "Number is out of range";
                    PlayerInputHandler.instance.DisplayCommands(message);
                }
                else if (PlayerInputHandler.instance._currentFolderGO.GetComponent<Device>().MyDeviceType == "elevator")
                {
                    PlayerInputHandler.instance._currentFolderGO.GetComponent<Device>().TypeFunctions("loop", args);
                }
            }
            else
            {
                string message = "Parameter has to be a number between 0 and 9";
                PlayerInputHandler.instance.DisplayCommands(message);
            }          
        }
        return true;
    }
}
