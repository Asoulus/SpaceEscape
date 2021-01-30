using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Elevator : Device
{
    [Header("Platform reference")]
    [SerializeField]
    private Platform _myPlatform = null;

    [SerializeField]
    private float speed = 5f;
    //[SerializeField]
    //private float delay = 5f;

    [Header("Booleans")]
    [SerializeField]
    private bool _isBot = false;
    [SerializeField]
    private bool _isTop = true;

    [SerializeField]
    private float _topHeight = 3f;
    [SerializeField]
    private float _bottomHeight = 0;

    [SerializeField]
    private int _repeat = 0;

    [SerializeField]
    private Renderer _renderer = null;
    [SerializeField]
    private Renderer _rendererPlatform = null;

    [SerializeField]
    private AudioSource _sound = null;

    private void Start()
    {
        ConnectPorts();

        PowerChange();

        //Loop(9);
    }

    public override void TypeFunctions(string value, string[] args)
    {
        switch (value)
        {
            case "loop":
                {
                    Loop(int.Parse(args[0]));
                }
                break;
            case "moveup":
                {
                    MoveUp();
                }
                break;
            case "movedown":
                {
                    MoveDown();
                }
                break;

        }
    }

    private void Loop(int repeat)
    {
        _repeat = repeat * 2;

        if (!_isTop)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    private void MoveUp()
    {
        if (_isBot && !_isTop)
        {
            _sound.Play();
            LeanTween.moveY(_myPlatform.gameObject, _topHeight, speed).setOnComplete(ToggleBools);   
        }
    }

    private void MoveDown()
    {
        if (!_isBot && _isTop)
        {
            _sound.Play();
            LeanTween.moveY(_myPlatform.gameObject, _bottomHeight, speed).setOnComplete(ToggleBools);         
        }
    }

    private void ToggleBools()
    {
        _sound.Stop();
        _isTop = !_isTop;
        _isBot = !_isBot;
        _repeat--;

        if ( _repeat > 0) 
        {
            StartCoroutine(PlatformDelay());
        }        
    }

    private IEnumerator PlatformDelay()
    {
        yield return new WaitForSeconds(2f);
        if (_isTop)
        {
            MoveDown();
        }
        else
        {
            MoveUp();
        }
    }

    public override void PowerChange()
    {
        Material[] tmp = _renderer.materials;
        Material[] tmpPlatform = _rendererPlatform.materials;

        if (_isPowered)
        {
            tmp[2] = _poweredMaterial;
            tmpPlatform[2] = _poweredMaterial;
            _renderer.materials = tmp;
            _rendererPlatform.materials = tmpPlatform;
        }
        else
        {
            tmp[2] = _unpoweredMaterial;
            tmpPlatform[2] = _unpoweredMaterial;
            _renderer.materials = tmp;
            _rendererPlatform.materials = tmpPlatform;
        }
    }
}
