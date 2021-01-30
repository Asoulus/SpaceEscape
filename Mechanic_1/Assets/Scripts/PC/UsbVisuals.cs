using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsbVisuals : MonoBehaviour
{
    [SerializeField]
    private string _mycolorCode;

    public string MyColor
    {
        get => _mycolorCode;
        set => _mycolorCode = value;
    }
}
