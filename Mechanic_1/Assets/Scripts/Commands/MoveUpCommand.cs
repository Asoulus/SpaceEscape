using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MoveUpCommand", menuName = "Commands/MoveUpCommands")]
public class MoveUpCommand : Command
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
                PlayerInputHandler.instance._currentFolderGO.GetComponent<Device>().TypeFunctions("moveup", args);
            }
        }
        return true;
    }
}
