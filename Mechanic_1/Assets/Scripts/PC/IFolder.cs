using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFolder 
{
    IEnumerable<ICommand> MyCommands { get; }

    void PerformCommandAction(string command, string[] agrs);

    void PlugUSB(USB usb);
}
