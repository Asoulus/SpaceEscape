using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Graviton : Device
{
    [Header("Renderers")]
    [SerializeField]
    private Renderer _frameRenderer = null;
    [SerializeField]
    private Renderer _crossRenderer = null;  
    [SerializeField]
    private Renderer _leftRenderer = null;  
    [SerializeField]
    private Renderer _rightRenderer = null; 
    [SerializeField]
    private Renderer _upRenderer = null;

    private Renderer _onRenderer = null;

    private Renderer _offRenderer = null;

    [Header("Pads")]
    [SerializeField]
    private GravityPad _gravityPadReverse = null;
    [SerializeField]
    private GravityPad _gravityPadRight = null;
    [SerializeField]
    private GravityPad _gravityPadLeft = null;
    [SerializeField]
    private GravityPad _gravityPadNormal = null;

    [Header("Indications")]
    [SerializeField]
    private GameObject _cross = null;
    [SerializeField]
    private GameObject _left = null;
    [SerializeField]
    private GameObject _right = null;
    [SerializeField]
    private GameObject _up = null;
    [SerializeField]
    private GameObject _on = null;
    [SerializeField]
    private GameObject _off = null;

    [Header("Is On")]
    [SerializeField]
    private bool _isOn = false;
    
    [Header("Sound")]
    [SerializeField]
    private AudioSource _sound = null;

    [Header("Player")]
    [SerializeField]
    private FirstPersonMovement _player;

    private Vector3 desiredgravity;

    private bool _canPress = false;

    private void Start()
    {
        ConnectPorts();
        //AssignPads();
        InputEventHandler.instance.onInteractionButtonPressed += InteractionButtonPressed;

        ToggleVisuals("cross");

        _crossRenderer = _cross.GetComponent<Renderer>();
        _leftRenderer = _left.GetComponent<Renderer>();
        _rightRenderer = _right.GetComponent<Renderer>();
        _upRenderer = _up.GetComponent<Renderer>();

        _onRenderer = _on.GetComponent<Renderer>();
        _offRenderer = _off.GetComponent<Renderer>();

        PowerChange();

        _player = PlayerReference.instance.GetComponent<FirstPersonMovement>();

        ToggleOnOff(_isOn);
    }

    public void AssignPads()
    {
        GravityPad[] tmp = GetComponentsInChildren<GravityPad>();

        _gravityPadReverse = tmp[0];
        _gravityPadLeft = tmp[1];
        _gravityPadRight = tmp[2];
        _gravityPadNormal = tmp[3];
        
    }
   
    public override void TypeFunctions(string value, string[] args)
    {
        switch (value)
        {
            case "gravity":
                {
                    if (args[0] == "reverse")
                    {
                        AssignDesiredGravity(_gravityPadReverse);
                        ToggleVisuals("up");
                    }

                    if (args[0] == "right")
                    {
                        AssignDesiredGravity(_gravityPadLeft);
                        ToggleVisuals("right");
                    }

                    if (args[0] == "left")
                    {
                        AssignDesiredGravity(_gravityPadRight);
                        ToggleVisuals("left");
                    }
                }
                break;
            case "turnoff":
                {
                    ToggleOnOff(false); 
                }
                break;
            case "turnon":
                {
                    AssignDesiredGravity(_gravityPadNormal);
                    ToggleVisuals("cross");
                    ToggleOnOff(true);
                    PowerChange();
                }
                break;
        }
    }

    private void ToggleOnOff(bool value)
    {
        _gravityPadReverse.IsOn = value;
        _isOn = value;

        if (value)
        {
            _on.SetActive(true);
            _off.SetActive(false);
        }
        else
        {
            _on.SetActive(false);
            _off.SetActive(true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isOn)
        {
            _canPress = true;
            MenuEventHandler.instance.Feedback("Press 'E' to interact.", 1f);
        }      
    }

    private void OnTriggerExit(Collider other)
    {
        _canPress = false;
    }

    private void ToggleVisuals(string name)
    {
        switch (name)
        {
            case "cross":
                {
                    _cross.SetActive(true);
                    _left.SetActive(false);
                    _right.SetActive(false);
                    _up.SetActive(false);

                }
                break; 
            case "left":
                {
                    _left.SetActive(true);
                    _right.SetActive(false);                   
                    _cross.SetActive(false);                 
                    _up.SetActive(false);

                }
                break;   
            case "right":
                {
                    _right.SetActive(true);
                    _left.SetActive(false);                                   
                    _cross.SetActive(false);                                    
                    _up.SetActive(false);

                }
                break; 
            case "up":
                {
                    _up.SetActive(true);
                    _right.SetActive(false);
                    _left.SetActive(false);
                    _cross.SetActive(false);                                    
                }
                break;
        }
    }

    private void InteractionButtonPressed()
    {
        if (_canPress)
        {
            ChangeGravity();
        }
    }

    private void AssignDesiredGravity(GravityPad activePad)
    {
        _gravityPadReverse.IsOn = true;
        desiredgravity = activePad.ReturnVector3();
    }

    public void ChangeGravity()
    {
        if (_gravityPadReverse.IsOn)
        {
            ChangeGravityAndRotatePlayer(desiredgravity, _gravityPadReverse);
        }
    }

    public void ChangeGravityAndRotatePlayer(Vector3 _playerRotationDirection, GravityPad pad)
    {
        _player._playerRotator.transform.up = _playerRotationDirection;
        _player.transform.position = pad.transform.position + new Vector3(0, 2f, 0);

        Physics.gravity = _player._playerRotator.transform.up * -9.81f; //zmiana grawitacji

        _sound.Play();
    }

    public override void PowerChange()
    {
        Material[] tmp = _frameRenderer.materials;

        if (_isPowered)
        {
            tmp[2] = _poweredMaterial;
            _frameRenderer.materials = tmp;

            _crossRenderer.material = _poweredMaterial;
            _leftRenderer.material = _poweredMaterial;
            _rightRenderer.material = _poweredMaterial;
            _upRenderer.material = _poweredMaterial;
            _onRenderer.material = _poweredMaterial;
            _offRenderer.material = _poweredMaterial;
        }
        else
        {
            tmp[2] = _unpoweredMaterial;
            _frameRenderer.materials = tmp;

            _crossRenderer.material = _unpoweredMaterial;
            _leftRenderer.material = _unpoweredMaterial;
            _rightRenderer.material = _unpoweredMaterial;
            _upRenderer.material = _unpoweredMaterial;
            _onRenderer.material = _unpoweredMaterial;
            _offRenderer.material = _unpoweredMaterial;
        }
    
    }

    private void OnDisable()
    {
        InputEventHandler.instance.onInteractionButtonPressed += InteractionButtonPressed;
    }
}
