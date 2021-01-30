using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GravityCommand", menuName = "Commands/GravityCommands")]
public class GravityCommand : Command
{
    public override bool Process(string[] args)
    {
        if (args.Length != 1)
        {
            WrongAmmountOfArguments();
        }
        else
        {
            if (PlayerInputHandler.instance._currentFolderGO.GetComponent<Device>().MyDeviceType == "graviton")
            {
                if (args[0] == "reverse" || args[0] == "right" || args[0] == "left")
                {
                    PlayerInputHandler.instance._currentFolderGO.GetComponent<Device>().TypeFunctions("gravity", args);
                }
                else
                {
                    string message = "Wrong parameter";
                    PlayerInputHandler.instance.DisplayCommands(message);
                }
                
            }
        }
        return true;
    }
}
