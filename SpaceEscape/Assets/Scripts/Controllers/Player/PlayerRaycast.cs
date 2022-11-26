using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerRaycast : MonoBehaviour
{
    public static PlayerRaycast instance;

    [SerializeField]
    private float _range = 200f;

    private Camera _playerCam;

    private bool _canPickUp;
    private bool _canPlugIn;

    [SerializeField]
    private bool _canPlugOut;

    public USB tmpUSB;
    public USBPort tmpUSBPort;
    public UsbVisuals tmpUSBVisual;

    public bool CanPickUp
    {
        get => _canPickUp;
        set => _canPickUp = value;
    }

    public bool CanPlugIn
    {
        get => _canPlugIn;
        set => _canPlugIn = value;
    }
    public bool CanPlugOut
    {
        get => _canPlugOut;
        set => _canPlugOut = value;
    } 

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _playerCam = GetComponent<Camera>();
        StartCoroutine(Counter());        
    }

    IEnumerator Counter()
    {
        yield return new WaitForSeconds(0.1f);
        CheckForInteractables();
        StartCoroutine(Counter());
    }

    private void CheckForInteractables()
    {
        RaycastHit hit;

        if (Physics.Raycast(_playerCam.transform.position, _playerCam.transform.forward, out hit, _range))
        {
            //Debug.Log(hit.collider.name);

            if (hit.collider.GetComponent<USB>())
            {
                CanPickUp = true;
                tmpUSB = hit.collider.GetComponent<USB>();
            }
            else
            {
                CanPickUp = false;
                tmpUSB = null;
            }


            if (hit.collider.GetComponent<USBPort>())
            {
                CanPlugIn = true;
                tmpUSBPort = hit.collider.GetComponent<USBPort>();
            }
            else
            {
                CanPlugIn = false;
                tmpUSBPort = null;
            }

            if (hit.collider.GetComponent<UsbVisuals>())
            {
                CanPlugOut = true;
                tmpUSBVisual = hit.collider.GetComponent<UsbVisuals>();
            }
            else
            {
                CanPlugOut = false;
                tmpUSBVisual = null;
            }
        }     
    }
}
