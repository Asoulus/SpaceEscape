using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder;
using System.Linq;
using System;
using UnityEngine.UI;

public class PCscript : MonoBehaviour, IFolder
{
    [Header("Name")]
    public string _myFileName;

    [Header("Commands")]
    public Command[] _myCommands = new Command[0];

    [Header("Refereces")]
    [SerializeField]
    private GameObject _onScreen = null;
    [SerializeField]
    private GameObject _offScreen = null;

    public GameObject _myPC;
    public TMP_InputField _myInput;
    public TMP_Text _myHistory;
    public TMP_Text _myDirectory;
    public TMP_Text _myInputText;
    public Scrollbar _myScroll;

    [Header("Devices")]
    [SerializeField]
    private List<Device> _myDevices = new List<Device>();
    
    [Header("USB Ports")]
    [SerializeField]
    private List<USBPort> _myUSBPorts = new List<USBPort>();

    [SerializeField]
    private FirstPersonMovement _player = null;

    [Header("Is On")]
    [SerializeField]
    private bool _isOn = false;

    public List<Device> MyDevices
    {
        get => _myDevices;
    }
    
    public List<USBPort> MyUSBPorts
    {
        get => _myUSBPorts;
    }

    [Header("PlayerInputHander")]
    private PlayerInputHandler playerInputHandler;

    private bool _canPress = false;
    private bool _using = false;

    public IEnumerable<ICommand> MyCommands => _myCommands;
    
    private void Start()
    {
        _myInput.characterLimit = 50;
        _myHistory.text = "Type <color=green>help</color> to see list of aviable commands\nType <color=green>help command</color> to see the discription of the <color=green>command</color>\n\n\n";
        _myDirectory.text += ">";

        InputEventHandler.instance.onInteractionButtonPressed += InteractionButtonPressed;
        InputEventHandler.instance.onEscapeButtonPressed += EscapeButtonPressed;
        InputEventHandler.instance.onConsoleExit += ConsoleExit;
        InputEventHandler.instance.onPlugUSBEvent += PlugUSBEvent;

        playerInputHandler = PlayerInputHandlerReference.instance.GetComponent<PlayerInputHandler>();

        ConnectPorts();

        _player = PlayerReference.instance.GetComponent<FirstPersonMovement>();

        ToggleScreen(false);
    }   

    private void EscapeButtonPressed()
    {
        if (_player.IsInteracting)
        {
            StartCoroutine(Delay());          
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        ConsoleExit();
    }

    private void ConsoleExit()
    {
        ToggleMovement(true);    
    }

    private void InteractionButtonPressed()
    {    
        if (_canPress)
        {        
            if (!_using)
            {           
                ToggleMovement(false);

                PlayerInputHandler.instance.AssignCurrentPC(this);
                PlayerInputHandlerReference.instance.GetComponent<PlayerInputHandler>().AssignCurrentPC(this.GetComponent<PCscript>());
            }                  
        }
    }

    private void OnTriggerEnter(Collider other) //sprawdza czy gracz stoi przy tym komputerze 
    {
        if (_isOn)
        {
            _canPress = true;
            MenuEventHandler.instance.Feedback("Press 'E' to interact.", 1f);
        }        
    }

    private void OnTriggerExit(Collider other) //sprawdza czy gracz stoi przy tym komputerze 
    {
        _canPress = false;
    }

    public void ToggleMovement(bool value)
    {
        PlayerReference.instance.GetComponent<PlayerJump>().enabled = value;
        _player.CanMove = value;    

        if (value) //finish coding
        {            
            _using = false;
            playerInputHandler.ClearConsole();
            _myInput.DeactivateInputField();           
            _player.IsInteracting = false;
            ToggleScreen(false);
        }
        else //start coding  
        {           
            _using = true;
            ToggleScreen(true);
            _myInput.ActivateInputField();
            _player.IsInteracting = true;
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

    public bool CheckUSBPlug(USB usb)
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

    public void PlugUSB(USB usb)
    {
        foreach (var port in _myUSBPorts)
        {
            if (port.MyUSB == null)
            {
                port.MyUSB = usb;
                port.MyUSB.MyFolder = this.GetComponent<IFolder>();//przypisuje folder
                InputEventHandler.instance.PlugUsbEvent(usb);
                break;
            }
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

    public void PlugUSBEvent(USB usb)
    {
        foreach (var port in _myUSBPorts)
        {
            if (port.MyUSB != null)
            {
                if (port.MyUSB.MyTwinUSB.MyDevice !=null )
                {
                    if (port.MyUSB.MyTwinUSB == usb) //przypadek jak usb najpierw do pc potem do urzadzenia
                    {
                        Device tmp = port.MyUSB.MyTwinUSB.MyDevice; 

                        bool viabilityCheck = true;

                        foreach (var device in MyDevices) //sprawdzenie czy to uzadzenie juz jest
                        {
                            if (device == tmp)
                            {
                                viabilityCheck = false;
                            }
                        }

                        if (viabilityCheck)
                        {
                            MyDevices.Add(tmp); //dodanie uzadzenia do listy sterowalnych urzadzen tego pc
                            tmp.AssignMyPc(this);
                            break;
                        }                     
                    }
                    else if (port.MyUSB.MyTwinUSB == usb.MyTwinUSB)//przypadek jak usb najpierw do urzadzenia potem pc
                    {
                        Device tmp = port.MyUSB.MyTwinUSB.MyDevice; 

                        bool viabilityCheck = true;

                        foreach (var device in MyDevices)//sprawdzenie czy to uzadzenie juz jest
                        {
                            if (device == tmp)
                            {
                                viabilityCheck = false;
                            }
                        }

                        if (viabilityCheck)
                        {
                            MyDevices.Add(tmp); //dodanie uzadzenia do listy sterowalnych urzadzen tego pc
                            tmp.AssignMyPc(this);
                            break;
                        }
                    }
                }              
            }          
        }
    }

    private IEnumerator UnplugUSBEvent(USB usb, string color)
    {
        yield return new WaitForSeconds(0.1f);
        //clearing info and references
        foreach (var port in _myUSBPorts)
        {
            if (port.MyUSB != null)
            {
                if (port.MyUSB.MyColorCode == color)
                {
                    MyDevices.Remove(port.MyUSB.MyTwinUSB.MyDevice); //usuwa uzadzenie z listy uzadzen tego komputera
                    port.MyUSB.MyDevice = null;
                    port.MyUSB.MyFolder = null;
                    port.MyUSB = null;
                    break;
                }          
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

    public void RemoveDevice(USB usb)
    {
        MyDevices.Remove(usb.MyDevice); //usuwa uzadzenie z listy uzadzen tego komputera
    }

    private void ConnectPorts()
    {
        USBPort[] tmp = this.GetComponentsInChildren<USBPort>();

        foreach (var port in tmp)
        {
            _myUSBPorts.Add(port);
        }
    }

    private void ToggleScreen(bool value)
    {
        _onScreen.SetActive(value);
        _offScreen.SetActive(!value);
    }

    private void OnDisable()
    {
        InputEventHandler.instance.onInteractionButtonPressed -= InteractionButtonPressed;
        InputEventHandler.instance.onEscapeButtonPressed -= EscapeButtonPressed;
        InputEventHandler.instance.onConsoleExit -= ConsoleExit;
        InputEventHandler.instance.onPlugUSBEvent -= PlugUSBEvent;
    }

}
