using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USBPort : MonoBehaviour
{
    [SerializeField]
    private USB _myUsb;

    public USB MyUSB
    {
        get => _myUsb;
        set => _myUsb = value;
    }
}
