using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Device : MonoBehaviour, IFolder
{
    [Header("Name")]
    [SerializeField]
    private string _myFileName = string.Empty;

    [Header("Type")]
    [SerializeField]
    private string _deviceType = string.Empty;

    [SerializeField]
    protected Command[] _myCommands = new Command[0];

    [SerializeField]
    private PCscript _myPC;

    [Header("USB Ports")]
    [SerializeField]
    protected List<USBPort> _myUSBPorts = new List<USBPort>();

    [Header("Power")]
    [SerializeField]
    protected bool _isPowered;

    [SerializeField]
    protected Material _poweredMaterial = null;
    [SerializeField]
    protected Material _unpoweredMaterial = null;

    public string MyFileName
    {
        get => _myFileName;
    }

    public bool IsPowered
    {
        get => _isPowered;
        set => _isPowered = value;
    }

    public string MyDeviceType
    {
        get => _deviceType;
    }
     
    protected PCscript MyPc
    {
        get => _myPC;
        set => _myPC = value;
    }

    public List<USBPort> MyUSBPorts
    {
        get => _myUSBPorts;
    }

    public IEnumerable<ICommand> MyCommands => _myCommands;

    private void Start()
    {
        ConnectPorts();
    }

    public abstract void TypeFunctions(string value, string[] args);   

    public abstract void PowerChange();

    public void AssignMyPc(PCscript pc)
    {       
        MyPc = pc;      
    }

    public void PlugUSB(USB usb)
    {
        foreach (var port in _myUSBPorts)
        {
            if (port.MyUSB == null)
            {
                port.MyUSB = usb;
                port.MyUSB.MyFolder = this.GetComponent<IFolder>(); //przypisuje folder
                port.MyUSB.MyDevice = this.GetComponent<Device>(); //przypisuje urzadzenie
                InputEventHandler.instance.PlugUsbEvent(usb);
                break;
            }
        }
    }

    public  bool CheckUSBPlug(USB usb)
    {
        foreach (var usbport in MyUSBPorts)
        {
            if (usbport.MyUSB != null)
            {
                if (usbport.MyUSB.MyColorCode == usb.MyColorCode)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void ConnectPorts()
    {
        USBPort[] tmp = this.GetComponentsInChildren<USBPort>();

        foreach (var port in tmp)
        {
            _myUSBPorts.Add(port);
        }
    }

    public USB UnplugUSB(string color)
    {
        foreach (var port in _myUSBPorts)
        {
            if (port.MyUSB != null)
            {
                if (port.MyUSB.MyColorCode == color)
                {
                    StartCoroutine(UnplugUSBEvent(port.MyUSB, color));
                    return port.MyUSB;
                }
            }
        }
        return null;
    }

    protected IEnumerator UnplugUSBEvent(USB usb, string color)
    {
        yield return new WaitForSeconds(0.1f);

        //clearing info and references
        foreach (var port in _myUSBPorts)
        {
            if (port.MyUSB != null && port.MyUSB == usb)
            {
                if (MyPc != null)
                {
                    MyPc.RemoveDevice(port.MyUSB);//usuwanie uzadzenia z listy komputera
                }

                MyPc = null; //usuwanie referencji do komputera
                port.MyUSB.MyDevice = null;
                port.MyUSB.MyFolder = null;
                port.MyUSB = null;
                break;
            }
        }

        //deleting visuals
        UsbVisuals[] tmpArray = this.GetComponentsInChildren<UsbVisuals>();

        for (int i = 0; i < tmpArray.Length; i++)
        {
            if (tmpArray[i].MyColor == color)
            {
                Destroy(tmpArray[i].gameObject);
                break;
            }
        }
    }

    public void PerformCommandAction(string command, string[] args)
    {
        List<ICommand> tmpList = _myCommands.ToList<ICommand>();

        for (int i = 0; i < tmpList.Count; i++)
        {
            if (string.Equals(tmpList[i].CommandWord, command))
            {
                tmpList[i].Process(args);
            }
        }
    }
}
