using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Stabilizer : Device
{
    [Header("Renderers")]
    [SerializeField]
    private Renderer _frame = null; 
    [SerializeField]
    private Renderer _arrow = null;
    [SerializeField]
    private Renderer _onRenderer = null;
    [SerializeField]
    private Renderer _offRenderer = null;

    [Header("Reference")]
    [SerializeField]
    private GameObject _field = null;
    [SerializeField]
    private GravityPad _pad = null;
    [SerializeField]
    private GameObject _on = null;
    [SerializeField]
    private GameObject _off = null;

    [Header("Sound")]
    [SerializeField]
    private AudioSource _fieldSound = null; 
    [SerializeField]
    private AudioSource _gravitySound = null;

    [Header("Is On")]
    [SerializeField]
    private bool _isOn = false;

    [Header("Player")]
    [SerializeField]
    private FirstPersonMovement _player;

    private bool _canPress = false;

    private void Start()
    {
        ConnectPorts();
        PowerChange();

        InputEventHandler.instance.onInteractionButtonPressed += InteractionButtonPressed;

        _player = PlayerReference.instance.GetComponent<FirstPersonMovement>();
        //_rotator = PlayerReference.instance.transform.parent.gameObject;

        _onRenderer = _onRenderer.GetComponent<Renderer>();
        _offRenderer = _offRenderer.GetComponent<Renderer>();

        if (_isOn)
        {
            _field.SetActive(true);
        }
        else
        {
            _field.SetActive(false);
        }

        ToggleOnOff(_isOn);
    }

    private void ToggleOnOff(bool value)
    {
        _pad.IsOn = value;
        _isOn = value;

        if (value)
        {
            _on.SetActive(true);
            _off.SetActive(false);
            _fieldSound.Play();
        }
        else
        {
            _on.SetActive(false);
            _off.SetActive(true);
            _fieldSound.Stop();
        }

    }

    private void InteractionButtonPressed()
    {
        if (_canPress)
        {
            AssignDesiredGravity();
            ChangeGravity();
        }
    }

    private void AssignDesiredGravity()
    {
        _pad.IsOn = true;
    }

    public void ChangeGravity()
    {
        Vector3 desiredgravity = new Vector3(0, 9.81f, 0);
        if (_pad.IsOn)
        {
            ChangeGravityAndRotatePlayer(desiredgravity, _pad);
        }
    }

    public void ChangeGravityAndRotatePlayer(Vector3 _playerRotationDirection, GravityPad pad)
    {
        _player._playerRotator.transform.up = _playerRotationDirection;
        _player.transform.position = pad.transform.position + new Vector3(0, 2f, 0);
        SwitchGravity();
        _gravitySound.Play();
    }

    private void SwitchGravity()
    {
        Physics.gravity = _player._playerRotator.transform.up * -9.81f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isOn)
        {
            _canPress = true;
            MenuEventHandler.instance.Feedback("Press 'E' to interact.\nRestore gravity", 1f);
        }  
    }

    private void OnTriggerExit(Collider other)
    {
        _canPress = false;
    }

    public override void PowerChange()
    {
        Material[] tmp = _frame.materials;

        if (_isPowered)
        {
            tmp[2] = _poweredMaterial;
            _frame.materials = tmp;
            _arrow.material = _poweredMaterial;
            _onRenderer.material = _poweredMaterial;
            _offRenderer.material = _poweredMaterial;
        }
        else
        {
            tmp[2] = _unpoweredMaterial;
            _frame.materials = tmp;
            _arrow.material = _unpoweredMaterial;
            _onRenderer.material = _unpoweredMaterial;
            _offRenderer.material = _unpoweredMaterial;
        }
    }

    public override void TypeFunctions(string value, string[] args)
    {
        switch (value)
        {
            case "turnoff":
                {
                    _isOn = false;
                    PowerChange();
                    _field.SetActive(false);
                    ToggleOnOff(false);
                }
                break;
            case "turnon":
                {
                    _isOn = true;
                    PowerChange();
                    _field.SetActive(true);
                    ToggleOnOff(true);
                }
                break;
        }
    }
   
    private void OnDisable()
    {
        InputEventHandler.instance.onInteractionButtonPressed += InteractionButtonPressed;
    }
}
