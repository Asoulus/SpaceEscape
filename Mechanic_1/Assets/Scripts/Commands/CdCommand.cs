using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CdCommand", menuName = "Commands/CdCommands")]
public class CdCommand : Command
{
    public override bool Process(string[] args)
    {
        if (args.Length != 1)
        {
            WrongAmmountOfArguments();
        }
        else
        {
            bool noDeviceCheck = true;

            if (args[0] == "..")
            {
                PlayerInputHandler.instance.AssignCurrentFolder(PlayerInputHandler.instance.CurrentPC.gameObject);
               
            }
            else
            {
                if (PlayerInputHandler.instance.CurrentPC != null)
                {
                    foreach (var device in PlayerInputHandler.instance.CurrentPC.MyDevices)
                    {
                        if (device.MyFileName == args[0])
                        {
                            
                            PlayerInputHandler.instance.AssignCurrentFolder(device.gameObject);
                         
                            noDeviceCheck = false;
                            break;
                        }
                    }

                    if (noDeviceCheck)
                    {
                        string message = "No <color=green>" + args[0] + "</color> device on this computer\n";
                        PlayerInputHandler.instance.DisplayCommands(message);
                    }
                }            
            }
        }
        return true;
    }
}
