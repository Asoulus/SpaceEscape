using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItems : MonoBehaviour
{
    [Header("Usb")]
    [SerializeField]
    private USB[] _myUSBs;

    [SerializeField]
    private int currentItemIndex = 0;

    public USB usb;

    [Header("Prefabs")]
    public GameObject greenUSBPrefab;
    public GameObject yellowUSBPrefab;
    public GameObject purpleUSBPrefab;
    public GameObject blueUSBPrefab;

    [Header("UI References")]
    [SerializeField]
    private Image imageOne = null;
    [SerializeField]
    private Image imageTwo = null;

    [SerializeField]
    private Image borderOne = null;
    [SerializeField]
    private Image borderTwo = null;

    [Header("Source Image")]
    [SerializeField]
    private Sprite _greenUSB = null;
    [SerializeField]
    private Sprite _yellowUSB = null;
    [SerializeField]
    private Sprite _purpleUSB = null;

    [SerializeField]
    private AudioSource _clickSound = null;

    

    private void Start()
    {
        InputEventHandler.instance.onLeftMouseButtonPressed += LeftMouseButtonPressed;
        InputEventHandler.instance.onOneButtonPressed += SelectItem;
        InputEventHandler.instance.onTwoButtonPressed += SelectItem;

        _myUSBs = new USB[2];

        DisplayUI();
        SelectItem(1);
    }

    private void LeftMouseButtonPressed()
    {
        if (PlayerRaycast.instance.CanPickUp)
        {
            if (_myUSBs[0] == null || _myUSBs[1] == null)
            {
                usb = PlayerRaycast.instance.tmpUSB;

                if (_myUSBs[0] == null)
                {
                    _myUSBs[0] = usb;
                }
                else
                {
                    _myUSBs[1] = usb;
                }

                usb.GetComponent<MeshRenderer>().enabled = false; //zniknięcie obiektu
                usb.GetComponent<BoxCollider>().enabled = false; 
                PlayerRaycast.instance.CanPickUp = false;

                DisplayUI();
                _clickSound.Play();

            }
            else
            {
                //Debug.Log("Nie ma miejsca na usb");
                MenuEventHandler.instance.Feedback("You don't have a free slot for USB",3f);
            }
                    
        }

        //wkładanie USB
        if (PlayerRaycast.instance.CanPlugIn)
        {
            PCscript tmpPC = PlayerRaycast.instance.tmpUSBPort.GetComponentInParent<PCscript>();
            Device tmpDevice = PlayerRaycast.instance.tmpUSBPort.GetComponentInParent<Device>();
            USBPort tmpPort = PlayerRaycast.instance.tmpUSBPort;

            if (_myUSBs[0] != null || _myUSBs[1] != null) 
            {
                bool colorCheck = false;
                bool currentSelectionCheck = true;

                //PC
                if (tmpPC != null)
                {
                    USB tmpUSB = _myUSBs[currentItemIndex];
                    
                    if (tmpUSB != null)
                    {                       
                        if (tmpPC.CheckUSBPlug(tmpUSB))
                        {
                            _myUSBs[currentItemIndex] = null; //wyjęcie z tablicy

                            tmpPC.PlugUSB(tmpUSB);
                            _clickSound.Play();

                            //visuals
                            switch (tmpUSB.MyColorCode)
                            {
                                case "green":
                                    {
                                        GameObject tmpGO = Instantiate(greenUSBPrefab, tmpPort.transform.position + (tmpPort.transform.forward * -0.005f), Quaternion.identity);
                                        tmpGO.transform.parent = tmpPC.gameObject.transform;
                                    }
                                    break;
                                case "yellow":
                                    {
                                        GameObject tmpGO = Instantiate(yellowUSBPrefab, tmpPort.transform.position + (tmpPort.transform.forward * -0.005f), Quaternion.identity);
                                        tmpGO.transform.parent = tmpPC.gameObject.transform;
                                    }
                                    break;
                                case "purple":
                                    {
                                        GameObject tmpGO = Instantiate(purpleUSBPrefab, tmpPort.transform.position + (tmpPort.transform.forward * -0.005f), Quaternion.identity);
                                        tmpGO.transform.parent = tmpPC.gameObject.transform;
                                    }
                                    break;
                                case "blue":
                                    {
                                        GameObject tmpGO = Instantiate(blueUSBPrefab, tmpPort.transform.position + (tmpPort.transform.forward * -0.005f), Quaternion.identity);
                                        tmpGO.transform.parent = tmpPC.gameObject.transform;
                                    }
                                    break;
                            }

                            colorCheck = true;
                            DisplayUI();
                        }
                    }
                    else
                    {
                        MenuEventHandler.instance.Feedback("You don't have a USB in the selected slot", 3f);
                        currentSelectionCheck = false;
                    }
                
                    if (!colorCheck && currentSelectionCheck)
                    {
                        MenuEventHandler.instance.Feedback("This color is already plugged into the computer", 3f);
                    }
                }
                else
                {
                    //Device
                    USB tmpUSB = _myUSBs[currentItemIndex];

                    if (tmpUSB != null)
                    {
                        if (tmpDevice.CheckUSBPlug(tmpUSB))
                        {
                            _myUSBs[currentItemIndex] = null; //wyjęcie z tablicy

                            tmpDevice.PlugUSB(tmpUSB);

                            _clickSound.Play();

                            switch (tmpUSB.MyColorCode)
                            {
                                case "green":
                                    {
                                        GameObject tmpGO = Instantiate(greenUSBPrefab, tmpPort.transform.position + (tmpPort.transform.forward * -0.005f), Quaternion.identity);

                                        Vector3 rot = tmpPort.transform.rotation.eulerAngles;
                                        rot = new Vector3(rot.x, rot.y + 90, rot.z);

                                        tmpGO.transform.rotation = Quaternion.Euler(rot);
                                        tmpGO.transform.parent = tmpDevice.gameObject.transform;
                                    }
                                    break;
                                case "yellow":
                                    {
                                        GameObject tmpGO = Instantiate(yellowUSBPrefab, tmpPort.transform.position + (tmpPort.transform.forward * -0.005f), Quaternion.identity);

                                        Vector3 rot = tmpPort.transform.rotation.eulerAngles;
                                        rot = new Vector3(rot.x, rot.y + 90, rot.z);

                                        tmpGO.transform.rotation = Quaternion.Euler(rot);
                                        tmpGO.transform.parent = tmpDevice.gameObject.transform;
                                    }
                                    break;
                                case "purple":
                                    {
                                        GameObject tmpGO = Instantiate(purpleUSBPrefab, tmpPort.transform.position + (tmpPort.transform.forward * -0.005f), Quaternion.identity);

                                        Vector3 rot = tmpPort.transform.rotation.eulerAngles;
                                        rot = new Vector3(rot.x, rot.y + 90, rot.z);

                                        tmpGO.transform.rotation = Quaternion.Euler(rot);
                                        tmpGO.transform.parent = tmpDevice.gameObject.transform;
                                    }
                                    break;
                                case "blue":
                                    {
                                        GameObject tmpGO = Instantiate(blueUSBPrefab, tmpPort.transform.position + (tmpPort.transform.forward * -0.005f), Quaternion.identity);

                                        Vector3 rot = tmpPort.transform.rotation.eulerAngles;
                                        rot = new Vector3(rot.x, rot.y + 90, rot.z);

                                        tmpGO.transform.rotation = Quaternion.Euler(rot);
                                        tmpGO.transform.parent = tmpDevice.gameObject.transform;
                                    }
                                    break;
                            }

                            colorCheck = true;
                            DisplayUI();
                        }
                    }
                    else
                    {
                        //Debug.Log("Nie masz usb w tym miejscu");
                        MenuEventHandler.instance.Feedback("You don't have a USB in the selected slot", 3f);
                        currentSelectionCheck = false;
                    }

                    if (!colorCheck && currentSelectionCheck)
                    {
                        //Debug.Log("ten kolor juz jest w PC");
                        MenuEventHandler.instance.Feedback("This color is already plugged into the computer",3f);
                    }
                }                       
            }
            else
            {
                //Debug.Log("Nie masz USB");
                MenuEventHandler.instance.Feedback("You don't have a USB", 3f);
            }
            
        }

        //wyjmowanie USB
        if (PlayerRaycast.instance.CanPlugOut) 
        {
            PCscript tmpPC = PlayerRaycast.instance.tmpUSBVisual.GetComponentInParent<PCscript>();
            Device tmpDevice = PlayerRaycast.instance.tmpUSBVisual.GetComponentInParent<Device>();
            UsbVisuals tmpVisuals = PlayerRaycast.instance.tmpUSBVisual;

            if (_myUSBs[0] == null || _myUSBs[1] == null)
            {
                //PC
                if (tmpPC != null)
                {
                    USB tmpUSB = _myUSBs[currentItemIndex];

                    if (tmpUSB == null) 
                    {
                        USB unpluggedUSB = tmpPC.UnplugUSB(tmpVisuals.MyColor);
                        _myUSBs[currentItemIndex] = unpluggedUSB;
                        DisplayUI();

                        _clickSound.Play();
                    }
                    else
                    {
                        //Debug.Log("Masz już usb w tym miejscu");
                        MenuEventHandler.instance.Feedback("You already have a USB in the selected slot", 3f);
                    }
                }
                else //Device
                {
                    USB tmpUSB = _myUSBs[currentItemIndex];

                    if (tmpUSB == null)
                    {
                        USB unpluggedUSB = tmpDevice.UnplugUSB(tmpVisuals.MyColor);
                        _myUSBs[currentItemIndex] = unpluggedUSB;
                        DisplayUI();

                        _clickSound.Play();
                    }
                    else
                    {
                        //Debug.Log("Masz już usb w tym miejscu");
                        MenuEventHandler.instance.Feedback("You already have a USB in the selected slot", 3f);
                    }
                }
                
            }
            else
            {
                //Debug.Log("Nie masz wolnego miejsca");
                MenuEventHandler.instance.Feedback("You don't have a free slot", 3f);
            }
        }

    }

    private void DisplayUI()
    {
        if (_myUSBs[0] == null && _myUSBs[1] == null)// [0][0]
        {
            imageOne.color = new Color(255, 255, 255, 0);          
            imageTwo.color = new Color(255, 255, 255, 0);
        }


        if (_myUSBs[0] != null && _myUSBs[1] == null)// [1][0]
        {
            switch (_myUSBs[0].MyColorCode)
            {
                case "green":
                    {
                        imageOne.sprite = _greenUSB;
                    }
                    break;
                case "yellow":
                    {
                        imageOne.sprite = _yellowUSB;
                    }
                    break; 
                case "purple":
                    {
                        imageOne.sprite = _purpleUSB;
                    }
                    break;
            }

            imageOne.color = new Color(255, 255, 255, 255);
            imageTwo.color = new Color(255, 255, 255, 0);
        }


        if (_myUSBs[0] == null && _myUSBs[1] != null)// [0][1]
        {
            switch (_myUSBs[1].MyColorCode)
            {
                case "green":
                    {
                        imageTwo.sprite = _greenUSB;
                    }
                    break;
                case "yellow":
                    {
                        imageTwo.sprite = _yellowUSB;
                    }
                    break;
                case "purple":
                    {
                        imageTwo.sprite = _purpleUSB;
                    }
                    break;
            }

            imageOne.color = new Color(255, 255, 255, 0);
            imageTwo.color = new Color(255, 255, 255, 255);
        }


        if (_myUSBs[0] != null && _myUSBs[1] != null)// [1][1]
        {

            switch (_myUSBs[0].MyColorCode)
            {
                case "green":
                    {
                        imageOne.sprite = _greenUSB;
                    }
                    break;
                case "yellow":
                    {
                        imageOne.sprite = _yellowUSB;
                    }
                    break;
                case "purple":
                    {
                        imageOne.sprite = _purpleUSB;
                    }
                    break;
            }

            switch (_myUSBs[1].MyColorCode)
            {
                case "green":
                    {
                        imageTwo.sprite = _greenUSB;
                    }
                    break;
                case "yellow":
                    {
                        imageTwo.sprite = _yellowUSB;
                    }
                    break;
                case "purple":
                    {
                        imageTwo.sprite = _purpleUSB;
                    }
                    break;
            }


            imageOne.color = new Color(255, 255, 255, 255);
            imageTwo.color = new Color(255, 255, 255, 255);
        }
    }

    private void SelectItem(int itemIndex)
    {
        if (itemIndex == 1)
        {
            currentItemIndex = 0;
            borderOne.gameObject.SetActive(true);
            borderTwo.gameObject.SetActive(false);
        }
        else if (itemIndex == 2)
        {
            currentItemIndex = 1;
            borderOne.gameObject.SetActive(false);
            borderTwo.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        InputEventHandler.instance.onInteractionButtonPressed -= LeftMouseButtonPressed;
        InputEventHandler.instance.onOneButtonPressed -= SelectItem;
        InputEventHandler.instance.onTwoButtonPressed -= SelectItem;
    } 
}
