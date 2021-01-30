using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireBox : MonoBehaviour
{
    [Header("Output")]
    [SerializeField]
    private WirePort _finalOutput = null;

    [Header("Lists")]
    [SerializeField]
    private List<Device> _myDevices = new List<Device>();

    [SerializeField]
    private List<Wire> _myWires = new List<Wire>(); 
    
    [SerializeField]
    private List<Gate> _myGates = new List<Gate>();

    [Header("Signal")]
    [SerializeField]
    private int _desiredSignal = 0; 

    [SerializeField]
    private int _storedSignal = 0;

    [Header("References")]
    [SerializeField]
    private GameObject _door = null;

    [SerializeField]
    private Camera _myCamera = null;

    [SerializeField]
    private FirstPersonMovement _player = null;

    [SerializeField]
    private ParticleSystem _sparks = null;

    [SerializeField]
    private AudioSource _discharge = null;

    [SerializeField]
    private AudioSource _powerUp = null;

    [SerializeField]
    private AudioSource _lever = null;

    private bool _canPress = false;
    private bool _using = false;

    [SerializeField]
    private CanvasGroup _mainUI = null;

    public int StoredSignal
    {
        get => _storedSignal;
    } 
    
    public int DesiredSignal
    {
        get => _desiredSignal;
    } 
    
    public bool Using
    {
        get => _using;
    }

    private void Start()
    {
        InputEventHandler.instance.onInteractionButtonPressed += InteractionButtonPressed;
        InputEventHandler.instance.onEscapeButtonPressed += EscapeButtonPressed;
        InputEventHandler.instance.onResetButtonPressed += ResetButtonPressed;

        _myCamera = GetComponentInChildren<Camera>();

        _player = PlayerReference.instance.GetComponent<FirstPersonMovement>();

        Wire[] tmpWires = GetComponentsInChildren<Wire>();
        Gate[] tmpGates = GetComponentsInChildren<Gate>();

        for (int i = 0; i < tmpWires.Length; i++)
        {
            _myWires.Add(tmpWires[i]);
        }

        for (int i = 0; i < tmpGates.Length; i++)
        {
            _myGates.Add(tmpGates[i]);
        }

        //_discharge = GetComponent<AudioSource>();
        _sparks = GetComponentInChildren<ParticleSystem>();
    }

    private void EscapeButtonPressed()
    {
        //InteractionButtonPressed();
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.125f);
        InteractionButtonPressed();
    }

    private void ResetButtonPressed()
    {
        if (_canPress)
        {
            if (_using)
            {
                foreach (var wire in _myWires)
                {
                    wire.ResetPosition();
                }

                foreach (var gate in _myGates)
                {
                    gate.ResetPosition();
                }
            }
        }    
    }

    private void InteractionButtonPressed()
    {
        if (_canPress)
        {
            if (!_using)
            {
                OpenDoor();
                _using = true;               
                _myCamera.depth = 1;

                _player.CanMove = false;
                PlayerReference.instance.GetComponent<PlayerJump>().enabled = false;
                _player.IsInteracting = true;

                _mainUI.alpha = 0;
                _mainUI.interactable = false;
                _mainUI.blocksRaycasts = false;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                CloseDoor();
                _using = false;
                
                _myCamera.depth = -1;

                _player.CanMove = true;
                PlayerReference.instance.GetComponent<PlayerJump>().enabled = true;
                _player.IsInteracting = false;

                _mainUI.alpha = 1;
                _mainUI.interactable = true;
                _mainUI.blocksRaycasts = true;

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

        }
    }

    public void SendSignalToDevices()
    {
        _lever.Play();

        bool wireCheck = true;
        foreach (var wire in _myWires)
        {
            if (!wire.Connected)
            {
                wireCheck = false;
            }
        }

        if (wireCheck)
        {
            if (_finalOutput.Signal == _desiredSignal)
            {
                foreach (var device in _myDevices)
                {
                    device.IsPowered = true;
                    device.PowerChange();
                }

                _powerUp.Play();
            }
            else
            {
                //info o niewlasciwym sygnale (visual)
                MenuEventHandler.instance.Feedback("Output signal does't match the desired signal.", 3f);

                _sparks.Play();
                _discharge.Play();

                foreach (var device in _myDevices)
                {
                    device.IsPowered = false;
                    device.PowerChange();
                }
            }
        }
        else
        {
            //info o niepolaczonych kablach (visual)
            MenuEventHandler.instance.Feedback("Not all wires are connected.", 3f);

            _sparks.Play();
            _discharge.Play();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        _canPress = true;

        MenuEventHandler.instance.Feedback("Press 'E' to interact.", 1f);
    }

    private void OnTriggerExit(Collider other)
    {
        _canPress = false;
    }

    private void OpenDoor()
    {
        _door.transform.eulerAngles = new Vector3(_door.transform.eulerAngles.x, _door.transform.eulerAngles.y, _door.transform.eulerAngles.z + 160f);
    } 
    
    private void CloseDoor()
    {
        _door.transform.eulerAngles = new Vector3(_door.transform.eulerAngles.x, _door.transform.eulerAngles.y, _door.transform.eulerAngles.z - 160f);
    }

    private void OnDisable()
    {
        InputEventHandler.instance.onInteractionButtonPressed -= InteractionButtonPressed;
        InputEventHandler.instance.onEscapeButtonPressed -= EscapeButtonPressed;
        InputEventHandler.instance.onResetButtonPressed -= ResetButtonPressed;
    }
}
