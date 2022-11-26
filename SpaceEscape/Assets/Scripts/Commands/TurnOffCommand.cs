using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurnOffCommand", menuName = "Commands/TurnOffCommands")]
public class TurnOffCommand : Command
{
    public override bool Process(string[] args)
    {
        if (args.Length != 0)
        {
            WrongAmmountOfArguments();
        }
        else
        {
            if (PlayerInputHandler.instance._currentFolderGO.GetComponent<Device>().MyDeviceType == "graviton")
            {             
                PlayerInputHandler.instance._currentFolderGO.GetComponent<Device>().TypeFunctions("turnoff", args);
            } 
            
            if (PlayerInputHandler.instance._currentFolderGO.GetComponent<Device>().MyDeviceType == "stabilizer")
            {             
                PlayerInputHandler.instance._currentFolderGO.GetComponent<Device>().TypeFunctions("turnoff", args);
            }
        }
        return true;
    }
}
