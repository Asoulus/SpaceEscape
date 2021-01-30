using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurnOnCommand", menuName = "Commands/TurnOnCommands")]
public class TurnOnCommand : Command
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
                PlayerInputHandler.instance._currentFolderGO.GetComponent<Device>().TypeFunctions("turnon", args);
            }

            if (PlayerInputHandler.instance._currentFolderGO.GetComponent<Device>().MyDeviceType == "stabilizer")
            {
                PlayerInputHandler.instance._currentFolderGO.GetComponent<Device>().TypeFunctions("turnon", args);
            }
        }
        return true;
    }
}
