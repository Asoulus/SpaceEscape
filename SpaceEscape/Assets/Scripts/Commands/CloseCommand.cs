using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CloseCommand", menuName = "Commands/CloseCommands")]
public class CloseCommand : Command
{
    public override bool Process(string[] args)
    {
        if (args.Length != 0)
        {
            WrongAmmountOfArguments();
        }
        else
        {
            if (PlayerInputHandler.instance._currentFolderGO.GetComponent<Device>().MyDeviceType == "door")
            {
                PlayerInputHandler.instance._currentFolderGO.GetComponent<Device>().TypeFunctions("close", args);
            }
        }
        return true;
    }
}
