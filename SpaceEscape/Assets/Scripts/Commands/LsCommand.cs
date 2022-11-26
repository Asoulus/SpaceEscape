using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LsCommand", menuName = "Commands/LsCommands")]
public class LsCommand : Command
{
    public override bool Process(string[] args)
    {
        if (args.Length > 0)
        {
            WrongAmmountOfArguments();
        }
        else
        {
            //InputEventHandler.instance.ConsoleExit(); TODO: display list of machines connected to current pc
            string nameList = string.Empty;

            if (PlayerInputHandler.instance.CurrentPC.MyDevices != null)
            {
                foreach (var device in PlayerInputHandler.instance.CurrentPC.MyDevices)
                {
                    nameList += "\n" + device.MyFileName;
                }
            }
            
            if (nameList == "")
            {
                nameList = "No divices attached to the computer";
            }

            PlayerInputHandler.instance.DisplayCommands(nameList);
        }

        return true;
    }
}
