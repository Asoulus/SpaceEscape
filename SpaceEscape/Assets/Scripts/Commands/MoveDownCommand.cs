using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveDownCommand", menuName = "Commands/MoveDownCommands")]
public class MoveDownCommand : Command
{
    public override bool Process(string[] args)
    {
        if (args.Length != 0)
        {
            WrongAmmountOfArguments();
        }
        else
        {
            if (PlayerInputHandler.instance._currentFolderGO.GetComponent<Device>().MyDeviceType == "elevator")
            {
                PlayerInputHandler.instance._currentFolderGO.GetComponent<Device>().TypeFunctions("movedown", args);
            }
        }
        return true;
    }
}
