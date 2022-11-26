using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Door : Device
{
    [Header("Door reference")]
    [SerializeField]
    private GameObject _door = null;
    [SerializeField]
    private GameObject _frame = null;

    private MeshRenderer _renderer = null;

    [SerializeField]
    private bool _isOpen = false;
    [SerializeField]
    private bool _isClosed = true;

    [SerializeField]
    private AudioSource _sound = null;

    [Header("Big door")]
    [SerializeField]
    private GameObject _leftDoor = null;
    [SerializeField]
    private GameObject _rightDoor = null;
    [SerializeField]
    private bool _isBig = false;
    [SerializeField]
    private bool _isRotated = false;

    private void Start()
    {
        ConnectPorts();

        _renderer = _frame.GetComponent<MeshRenderer>();
        PowerChange();

        _sound = GetComponent<AudioSource>();
    }

    public override void TypeFunctions(string value, string[] args)
    {
        switch (value)
        {
            case "open":
                {
                    OpenDoor();
                }
                break;
            case "close":
                {
                    CloseDoor();
                }
                break;
        }
    }

    private void OpenDoor()
    {
        if (_isPowered)
        {
            if (_isClosed)
            {
                if (!_isBig)
                {
                    if (_door != null)
                    {
                        LeanTween.moveLocal(_door, -_door.transform.right * 5, 2f);
                        _sound.Play();
                    }            
                }
                else
                {
                    if (_leftDoor != null && _rightDoor != null) 
                    {
                        if (_isRotated)
                        {
                            LeanTween.moveLocal(_rightDoor, _rightDoor.transform.up * 5, 2f);
                            LeanTween.moveLocal(_leftDoor, _leftDoor.gameObject.transform.up * 5, 2f);
                        }
                        else
                        {
                            LeanTween.moveLocal(_rightDoor, _rightDoor.gameObject.transform.right * 5, 2f);
                            LeanTween.moveLocal(_leftDoor, _leftDoor.gameObject.transform.right * 5, 2f);
                        }
                       
                        _sound.Play();
                    }
                   
                }
              

                _isOpen = true;
                _isClosed = !_isOpen;
            }
        }
        else
        {
            //comunikat o braku energii
            MenuEventHandler.instance.Feedback("The device doesn't have power", 2f);
        }
          
    }
    
    private void CloseDoor()
    {
        if (_isPowered)
        {
            if (_isOpen)
            {
                if (!_isBig)
                {
                    if (_door != null)
                    {
                        LeanTween.moveLocal(_door, _door.transform.right * 0, 2f);
                        _sound.Play();
                    }                        
                }
                else
                {
                    if (_leftDoor != null && _rightDoor != null)
                    {
                        LeanTween.moveLocal(_rightDoor, -_door.transform.right * 5, 2f);
                        LeanTween.moveLocal(_leftDoor, _door.transform.right * 5, 2f);
                        _sound.Play();
                    }                 
                }


                _isOpen = false;
                _isClosed = !_isOpen;
            }
        }
        else
        {
            //comunikat o braku energii
            MenuEventHandler.instance.Feedback("The device doesn't have power", 2f);
        }

    }

    public override void PowerChange()
    {
        Material[] tmp = _renderer.materials;

        if (_isPowered)
        {    
            tmp[2] = _poweredMaterial;
            _renderer.materials = tmp;
        }
        else
        {
            tmp[2] = _unpoweredMaterial;
            _renderer.materials = tmp;
        }
    }
}
