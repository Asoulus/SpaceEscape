using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USB : MonoBehaviour
{
    [SerializeField]
    private USB _myTwinUsb;

    [SerializeField]
    private IFolder _myFolder;
    
    [SerializeField]
    private string _mycolorCode = string.Empty;
    
    [SerializeField]
    private Device _myDevice;


    public USB MyTwinUSB
    {
        get => _myTwinUsb;
        set => _myTwinUsb = value;
    }

    public IFolder MyFolder
    {
        get => _myFolder;
        set => _myFolder = value;
    } 
    
    public Device MyDevice
    {
        get => _myDevice;
        set => _myDevice = value;
    }
    
    public string MyColorCode
    {
        get => _mycolorCode;
    }
}
